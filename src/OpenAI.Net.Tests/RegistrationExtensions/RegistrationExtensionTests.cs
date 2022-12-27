using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class RegistrationExtensionTests
    {
        [Test]
        public void ServiceCollection_AddOpenAIServices()
        {
            var apiUrl = "https://api.openai.com/";
            var apiKey = "ApiKey";
            var organizationId = "OrgId";
            bool httpClientOptionsCalled = false;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOpenAIServices(apiKey,  organizationId, apiUrl, (httpClientOptions) => {
                if (httpClientOptions != null)
                {
                    httpClientOptionsCalled = true;
                }
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var openAIService = serviceProvider.GetService<IOpenAIService>();

            Assert.NotNull(openAIService, "OpenAIService was not resolved from container");
            Assert.NotNull(openAIService.Models, "OpenAIService.Models was null");
            Assert.NotNull(openAIService.Images, "OpenAIService.Images was null");
            Assert.NotNull(openAIService.Files, "OpenAIService.Files was null");
            Assert.NotNull(openAIService.Embeddings, "OpenAIService.Embeddings was null");
            Assert.NotNull(openAIService.FineTune, "OpenAIService.FineTune was null");
            Assert.NotNull(openAIService.TextCompletion, "OpenAIService.TextCompletion was null");
            Assert.NotNull(openAIService.TextEdit, "OpenAIService.TextEdit was null");
            Assert.NotNull(openAIService.Moderation, "OpenAIService.Moderation was null");

            Assert.True(httpClientOptionsCalled, "HttpClientOptions not called");

            var service = (openAIService.Moderation as ModerationService);
            Assert.That(service.HttpClient.BaseAddress.ToString(), Is.EqualTo(apiUrl));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Scheme, Is.EqualTo("Bearer"));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Parameter, Is.EqualTo(apiKey));
            Assert.That(service.HttpClient.DefaultRequestHeaders.FirstOrDefault(i=> i.Key == "OpenAI-Organization").Value.First(), Is.EqualTo(organizationId));
        }

        [Test]
        public void ServiceCollection_AddOpenAIServicesWithOptions()
        {
            var apiUrl = "https://api.openai.com/";
            var apiKey = "ApiKey";
            var organizationId = "OrgId";
            bool httpClientOptionsCalled = false;
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddOpenAIServices(o => { 
                o.ApiKey = apiKey;
                o.ApiUrl = apiUrl;
                o.OrganizationId = organizationId;
            },(httpClientOptions) => {
                if (httpClientOptions != null)
                {
                    httpClientOptionsCalled = true;
                }
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var openAIService = serviceProvider.GetService<IOpenAIService>();

            Assert.NotNull(openAIService, "OpenAIService was not resolved from container");
            Assert.NotNull(openAIService.Models, "OpenAIService.Models was null");
            Assert.NotNull(openAIService.Images, "OpenAIService.Images was null");
            Assert.NotNull(openAIService.Files, "OpenAIService.Files was null");
            Assert.NotNull(openAIService.Embeddings, "OpenAIService.Embeddings was null");
            Assert.NotNull(openAIService.FineTune, "OpenAIService.FineTune was null");
            Assert.NotNull(openAIService.TextCompletion, "OpenAIService.TextCompletion was null");
            Assert.NotNull(openAIService.TextEdit, "OpenAIService.TextEdit was null");
            Assert.NotNull(openAIService.Moderation, "OpenAIService.Moderation was null");

            Assert.True(httpClientOptionsCalled, "HttpClientOptions not called");

            var service = (openAIService.Moderation as ModerationService);
            Assert.That(service.HttpClient.BaseAddress.ToString(), Is.EqualTo(apiUrl));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Scheme, Is.EqualTo("Bearer"));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Parameter, Is.EqualTo(apiKey));
            Assert.That(service.HttpClient.DefaultRequestHeaders.FirstOrDefault(i => i.Key == "OpenAI-Organization").Value.First(), Is.EqualTo(organizationId));
        }

    }
}
