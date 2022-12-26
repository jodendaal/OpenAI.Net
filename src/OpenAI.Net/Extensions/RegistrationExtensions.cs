using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Models;
using OpenAI.Net.Services;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddOpenAIServices(this IServiceCollection services, Action<OpenAIServiceRegistrationOptions> options)
        {
            var optionsInstance = new OpenAIServiceRegistrationOptions();
            options.Invoke(optionsInstance);

            OpenAIDefaults.ApiUrl = optionsInstance.ApiUrl;
            OpenAIDefaults.TextCompletionModel = optionsInstance.Defaults.TextCompletionModel;
            OpenAIDefaults.TextEditModel = optionsInstance.Defaults.TextEditModel;

            services.AddOpenAIServices(optionsInstance.ApiKey, optionsInstance.OrganizationId, optionsInstance.ApiUrl);
            return services;
        }

        public static IServiceCollection AddOpenAIServices(this IServiceCollection services,string apiKey, string? organization = null,string apiUrl = "https://api.openai.com/")
        {
            Action<HttpClient> configureClient = (c) => {
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                if (!string.IsNullOrEmpty(organization))
                {
                    c.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organization}");
                }
            };

            services.AddHttpClient<IModelsService, ModelsService>(configureClient);
            services.AddHttpClient<ITextCompletionService, TextCompletionService>(configureClient);
            services.AddHttpClient<ITextEditService, TextEditService>(configureClient);
            services.AddHttpClient<IImageService, ImageService>(configureClient);
            services.AddHttpClient<IFilesService, FilesService>(configureClient);
            services.AddHttpClient<IFineTuneService, FineTuneService>(configureClient);
            services.AddHttpClient<IModerationService, ModerationService>(configureClient);
            services.AddHttpClient<IEmbeddingsService, EmbeddingsService>(configureClient);

            services.AddTransient<IOpenAIService, OpenAIService>();
            return services;
        }
    }
}
