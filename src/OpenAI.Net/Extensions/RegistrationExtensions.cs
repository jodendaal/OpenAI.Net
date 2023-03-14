using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Models;
using OpenAI.Net.Services;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddOpenAIServices(this IServiceCollection services, Action<OpenAIServiceRegistrationOptions> options, Action<IHttpClientBuilder> httpClientBuilderOptions = default!)
        {
            var optionsInstance = new OpenAIServiceRegistrationOptions();
            options.Invoke(optionsInstance);

            OpenAIDefaults.ApiUrl = optionsInstance.ApiUrl;
            OpenAIDefaults.TextCompletionModel = optionsInstance.Defaults.TextCompletionModel;
            OpenAIDefaults.TextEditModel = optionsInstance.Defaults.TextEditModel;
            OpenAIDefaults.EmbeddingsModel = optionsInstance.Defaults.EmbeddingsModel;

            services.AddOpenAIServices(optionsInstance.ApiKey, optionsInstance.OrganizationId, optionsInstance.ApiUrl, httpClientBuilderOptions);
            return services;
        }

        public static IServiceCollection AddOpenAIServices(this IServiceCollection services, string apiKey, string? organization = null, string apiUrl = "https://api.openai.com/", Action<IHttpClientBuilder> httpClientOptions = default!)
        {
            Action<HttpClient> configureClient = (c) =>
            {
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                if (!string.IsNullOrEmpty(organization))
                {
                    c.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organization}");
                }
            };

            ConfigureHttpClientBuilder(services.AddHttpClient<IModelsService, ModelsService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<ITextCompletionService, TextCompletionService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<ITextEditService, TextEditService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IImageService, ImageService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IFilesService, FilesService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IFineTuneService, FineTuneService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IModerationService, ModerationService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IEmbeddingsService, EmbeddingsService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IChatCompletionService, ChatCompletionService>(configureClient), httpClientOptions);

            services.AddTransient<IOpenAIService, OpenAIService>();
            return services;
        }

        private static void ConfigureHttpClientBuilder(IHttpClientBuilder clientBuilder, Action<IHttpClientBuilder> action)
        {
            action?.Invoke(clientBuilder);
        }
    }
}
