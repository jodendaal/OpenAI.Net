using AutoFixture;
using AutoFixture.Dsl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Models.Requests;
using System.Linq.Expressions;
using System.Text.Json;
using WireMock.Server;

namespace OpenAI.Net.Acceptance.Tests
{
    public class BaseTest : IDisposable
    {
        public readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public readonly WireMockServer WireMockServer;
        public TestConfig Config { get; private set; }

        private IServiceProvider _serviceProvider;
        public BaseTest()
        {
            WireMockServer = WireMockServer.Start();
            Config = new TestConfig
            {
                Apikey = "ABC",
                ApiUrl = WireMockServer?.Url ?? "",
                OrganizationId = "My-Org-Id"
            };

            var configuration = new ConfigurationBuilder()
           .AddUserSecrets<BaseTest>()
           .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenAIServices(options => {
                options.ApiKey = Config.Apikey;
                options.ApiUrl = Config.ApiUrl;
                options.OrganizationId = Config.OrganizationId;
            });

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public IOpenAIService OpenAIService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IOpenAIService>();
            }
        }

        public T CreateObjectWithRandomData<T>()
        {
            var fixture = new Fixture();
            return fixture.Create<T>();
        }

        public void Dispose()
        {
            this.WireMockServer.Dispose();
        }
    }


    public class TestConfig
    {
        public string Apikey { get;  set; }
        public string ApiUrl { get;  set; }
        public string OrganizationId { get;  set; }
    }

    public static class AutoFixtureExtensions
    {
        public static IPostprocessComposer<T> WithValues<T, TProperty>(this IPostprocessComposer<T> composer,
        Expression<Func<T, TProperty>> propertyPicker,
        params TProperty[] values)
        {
            var queue = new Queue<TProperty>(values);

            return composer.With(propertyPicker, () => queue.Dequeue());
        }
    }
         

}
