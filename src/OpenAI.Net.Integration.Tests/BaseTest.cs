using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    public class BaseTest
    {
        public TestConfig Config { get; private set; }
        private IServiceProvider _serviceProvider;

        public BaseTest()
        {
            var configuration = new ConfigurationBuilder()
            .AddUserSecrets<BaseTest>()
            .AddEnvironmentVariables()
            .Build();

            Config = new TestConfig(configuration);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenAIServices(options => {
                options.ApiKey = Config.Apikey;
            },
            (httpClientOptions) => {
                httpClientOptions.AddPolicyHandler(GetRetryPolicy());
            });
            _serviceProvider  =serviceCollection.BuildServiceProvider();
        }

        public IOpenAIService OpenAIService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IOpenAIService>();
            }
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var policy = Policy<HttpResponseMessage>
                .HandleResult(response => response.StatusCode == HttpStatusCode.TooManyRequests)
                .OrTransientHttpError();

            return policy
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(30));
        }
    }

   



    public class TestConfig
    {
        public TestConfig(IConfigurationRoot configuration)
        {
            Apikey = configuration["Apikey"];
            ApiUrl = configuration["ApiUrl"];
        }

        public string Apikey { get; private set; }
        public string ApiUrl { get; private set; }
    }
}
