using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Brokers;
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

            ConfigureHttpClientBuilder(services.AddHttpClient<IHttpService, HttpService>(configureClient), httpClientOptions);

            services.AddTransient<ITextCompletionBroker, TextCompletionBroker>();
            services.AddTransient<ITextCompletionService, TextCompletionService>();

            services.AddTransient<IEmbeddingsService, EmbeddingsService>();
            services.AddTransient<IEmbeddingsBroker, EmbeddingsBroker>();

            ConfigureHttpClientBuilder(services.AddHttpClient<IModelsService, ModelsService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<ITextEditService, TextEditService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IImageService, ImageService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IFilesService, FilesService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IFineTuneService, FineTuneService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IModerationService, ModerationService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IChatCompletionService, ChatCompletionService>(configureClient), httpClientOptions);
            ConfigureHttpClientBuilder(services.AddHttpClient<IAudioService, AudioService>(configureClient), httpClientOptions);

            services.AddTransient<IOpenAIService, OpenAIService>();
            return services;
        }

        public static IServiceCollection AddTextCompletionServiceAzure(this IServiceCollection services, Action<OpenAIServiceAzureRegistrationOptions> options, Action<IHttpClientBuilder> httpClientOptions = default!)
        {
            AzureOpenAIConfig config = ConfigureAzure(services, options, httpClientOptions, nameof(ITextCompletionService));

            services.AddTransient<ITextCompletionBroker>(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>()!;
                var httpclient = httpClientFactory.CreateClient(nameof(ITextCompletionService));
                var httpService = new HttpService(httpclient);
                return new TextCompletionAzureBroker(httpService, config);
            });

            services.AddTransient<ITextCompletionService, TextCompletionService>();

            return services;
        }

        public static IServiceCollection AddOpenAIEmbeddingsAzureService(this IServiceCollection services, Action<OpenAIServiceAzureRegistrationOptions> options, Action<IHttpClientBuilder> httpClientOptions = default!)
        {
            AzureOpenAIConfig config = ConfigureAzure(services, options, httpClientOptions, nameof(IEmbeddingsService));

            services.AddTransient<IEmbeddingsBroker>(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>()!;
                var httpclient = httpClientFactory.CreateClient(nameof(IEmbeddingsService));
                var httpService = new HttpService(httpclient);
                return new EmbeddingsAzureBroker(httpService, config);
            });

            services.AddTransient<IEmbeddingsService, EmbeddingsService>();

            return services;
        }

        private static AzureOpenAIConfig ConfigureAzure(IServiceCollection services, Action<OpenAIServiceAzureRegistrationOptions> options, Action<IHttpClientBuilder> httpClientOptions,string httpClientName)
        {
            var optionsInstance = new OpenAIServiceAzureRegistrationOptions();
            options.Invoke(optionsInstance);

            OpenAIDefaults.TextCompletionModel = optionsInstance.Defaults.TextCompletionModel;
            OpenAIDefaults.TextEditModel = optionsInstance.Defaults.TextEditModel;
            OpenAIDefaults.EmbeddingsModel = optionsInstance.Defaults.EmbeddingsModel;

            Action<HttpClient> configureClient = (httpClient) =>
            {
                httpClient.BaseAddress = new Uri(optionsInstance.ApiUrl);
                httpClient.DefaultRequestHeaders.Add("api-key", optionsInstance.ApiKey);
            };

            var config = new AzureOpenAIConfig()
            {
                ApiVersion = optionsInstance.ApiVersion,
                DeploymentName = optionsInstance.DeploymentName,
                ApiUrl = optionsInstance.ApiUrl
            };

            ConfigureHttpClientBuilder(services.AddHttpClient<IHttpService, HttpService>(httpClientName, configureClient), httpClientOptions);
            return config;
        }

        private static void ConfigureHttpClientBuilder(IHttpClientBuilder clientBuilder, Action<IHttpClientBuilder> action)
        {
            action?.Invoke(clientBuilder);
        }
    }
}
