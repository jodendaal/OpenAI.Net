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

            Assert.That(openAIService, Is.Not.Null, "OpenAIService was not resolved from container");
            Assert.That(openAIService.Models, Is.Not.Null, "OpenAIService.Models was null");
            Assert.That(openAIService.Images, Is.Not.Null, "OpenAIService.Images was null");
            Assert.That(openAIService.Files, Is.Not.Null, "OpenAIService.Files was null");
            Assert.That(openAIService.Embeddings, Is.Not.Null, "OpenAIService.Embeddings was null");
            Assert.That(openAIService.FineTune, Is.Not.Null, "OpenAIService.FineTune was null");
            Assert.That(openAIService.TextCompletion, Is.Not.Null, "OpenAIService.TextCompletion was null");
            Assert.That(openAIService.TextEdit, Is.Not.Null, "OpenAIService.TextEdit was null");
            Assert.That(openAIService.Moderation, Is.Not.Null, "OpenAIService.Moderation was null");
            Assert.That(openAIService.Chat, Is.Not.Null, "OpenAIService.Chat was null");
            Assert.That(openAIService.Audio, Is.Not.Null, "OpenAIService.Audio was null");

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

            Assert.That(openAIService, Is.Not.Null, "OpenAIService was not resolved from container");
            Assert.That(openAIService.Models, Is.Not.Null, "OpenAIService.Models was null");
            Assert.That(openAIService.Images, Is.Not.Null, "OpenAIService.Images was null");
            Assert.That(openAIService.Files, Is.Not.Null, "OpenAIService.Files was null");
            Assert.That(openAIService.Embeddings, Is.Not.Null, "OpenAIService.Embeddings was null");
            Assert.That(openAIService.FineTune, Is.Not.Null, "OpenAIService.FineTune was null");
            Assert.That(openAIService.TextCompletion, Is.Not.Null, "OpenAIService.TextCompletion was null");
            Assert.That(openAIService.TextEdit, Is.Not.Null, "OpenAIService.TextEdit was null");
            Assert.That(openAIService.Moderation, Is.Not.Null, "OpenAIService.Moderation was null");
            Assert.That(openAIService.Chat, Is.Not.Null, "OpenAIService.Chat was null");
            Assert.That(openAIService.Audio, Is.Not.Null, "OpenAIService.Audio was null");

            Assert.True(httpClientOptionsCalled, "HttpClientOptions not called");

            var service = (openAIService.Moderation as ModerationService);
            Assert.That(service.HttpClient.BaseAddress.ToString(), Is.EqualTo(apiUrl));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Scheme, Is.EqualTo("Bearer"));
            Assert.That(service.HttpClient.DefaultRequestHeaders.Authorization.Parameter, Is.EqualTo(apiKey));
            Assert.That(service.HttpClient.DefaultRequestHeaders.FirstOrDefault(i => i.Key == "OpenAI-Organization").Value.First(), Is.EqualTo(organizationId));
        }

    }
}
