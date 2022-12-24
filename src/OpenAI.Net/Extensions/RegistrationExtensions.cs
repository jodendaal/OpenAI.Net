using Microsoft.Extensions.DependencyInjection;
using OpenAI.Net.Services;
using OpenAI.Net.Services.Interfaces;
using System.Net.Http;

namespace OpenAI.Net.Extensions
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddOpenAIServices(this IServiceCollection services,string apiKey,string apiUrl,string? organization = null)
        {
            Action<HttpClient> configureClient = (c) => {
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                if (!string.IsNullOrEmpty(organization))
                {
                    c.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organization}"); ;
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
