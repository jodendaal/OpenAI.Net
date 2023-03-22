using AutoFixture;
using AutoFixture.Dsl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
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
        public readonly WireMockServer? WireMockServer;
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

        public void ConfigureWireMockPostJson<TReqeust,TResponse>(string path,TReqeust reqeust,TResponse response)
        {
            var body = (response is string) ? response as string :
                     JsonSerializer.Serialize(
                response,
                this.JsonSerializerOptions);

            this.WireMockServer?.Given(
              Request.Create()
              .WithPath(path)
              .WithHeader("Authorization", $"Bearer {Config.Apikey}")
              .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
              .WithHeader("Content-Type", "application/json; charset=utf-8")
              .UsingPost()
              .WithBody(JsonSerializer.Serialize(
                  reqeust,
                  this.JsonSerializerOptions)))
              .RespondWith(
                 Response.Create()
              .WithBody(body??""));
        }

        public void ConfigureWireMockPostForm<TReqeust, TResponse>(string path, TReqeust reqeust, TResponse response)//QQQ Come back this this and check if we can validate header and contents
        {
            var body = (response is string) ? response as string :
                    JsonSerializer.Serialize(
               response,
               this.JsonSerializerOptions);

            var formData = reqeust?.ToMultipartFormDataContent();

            this.WireMockServer?.Given(
              Request.Create()
              .WithPath(path)
              .WithHeader("Authorization", $"Bearer {Config.Apikey}")
              .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
              .UsingPost()
              //.WithHeader("Content-Type", "multipart/form-data")
              //.WithBody(formData)
              )
               
              .RespondWith(
                 Response.Create()
              .WithBody(body ?? ""));
        }


        public void ConfigureWireMockDelete<TResponse>(string path, TResponse response)
        {

            var body = (response is string) ? response as string :
                    JsonSerializer.Serialize(
               response,
               this.JsonSerializerOptions);

            this.WireMockServer?.Given(
              Request.Create()
              .WithPath(path)
              .WithHeader("Authorization", $"Bearer {Config.Apikey}")
              .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
              .UsingDelete())
              .RespondWith(
                 Response.Create()
              .WithBody(body ?? ""));
        }

        public void ConfigureWireMockGet<TResponse>(string path, TResponse response)
        {
            var body = (response is string) ? response as string :
                   JsonSerializer.Serialize(
              response,
              this.JsonSerializerOptions);

            this.WireMockServer?.Given(
              Request.Create()
              .WithPath(path)
              .WithHeader("Authorization", $"Bearer {Config.Apikey}")
              .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
              .UsingGet())
              .RespondWith(
                 Response.Create()
              .WithBody(body ?? ""));
        }

        public void Dispose()
        {
            this.WireMockServer?.Dispose();
        }
    }


    public class TestConfig
    {
        public string? Apikey { get;  set; }
        public string? ApiUrl { get;  set; }
        public string? OrganizationId { get;  set; }
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
