using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Extensions;

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
            .Build();

            Config = new TestConfig(configuration);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenAIServices(Config.Apikey);
            _serviceProvider  =serviceCollection.BuildServiceProvider();
        }

        public IOpenAIService OpenAIService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IOpenAIService>();
            }
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
