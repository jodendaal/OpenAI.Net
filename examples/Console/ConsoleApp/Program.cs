using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;
using OpenAI.Net.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddUserSecrets(typeof(Program).Assembly);
            })
            .ConfigureServices((builder, services) =>
            {
                //services.AddOpenAIAzureServices(options => {
                //    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                //    options.ApiUrl = "https://azo-ai-leg.openai.azure.com";
                //    options.DeploymentName = "text-davinci-002";
                //    options.Defaults.TextCompletionModel = ModelTypes.TextDavinci002;
                //});

                services.AddTextCompletionServiceAzure(options =>
                {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                    options.ApiUrl = "https://azo-ai-leg.openai.azure.com";
                    options.DeploymentName = "text-davinci-002";
                    options.Defaults.TextCompletionModel = ModelTypes.TextDavinci002;
                },httpOptions => {
                    httpOptions.AddPolicyHandler(GetRetryPolicy());
                });

                services.AddOpenAIEmbeddingsAzureService(options =>
                {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                    options.ApiUrl = "https://azo-ai-leg.openai.azure.com";
                    options.DeploymentName = "text-embedding-ada-002";
                }, httpOptions => {
                    httpOptions.AddPolicyHandler(GetRetryPolicy());
                });
            })
            .Build();

            var openAi = host.Services.GetService<ITextCompletionService>()!;
            var response = await openAi.Get("How long until we reach mars?", o => {
                o.Model = ModelTypes.TextDavinci003;
            });

            if (response.IsSuccess)
            {
                foreach (var result in response.Result.Choices)
                {
                    Console.WriteLine(result.Text);
                }
            }
            else
            {
                Console.WriteLine($"{response.ErrorMessage}");
            }

            var embeddingsService = host.Services.GetService<IEmbeddingsService>()!;
            var embeddingsResponse = await embeddingsService.Create("How long until we reach mars?");

            if (embeddingsResponse.IsSuccess)
            {
                Console.WriteLine(embeddingsResponse.Result.Data);
            }
            else
            {
                Console.WriteLine($"{embeddingsResponse.ErrorMessage}");
            }



            //var openAi = host.Services.GetService<IOpenAIService>();
            //var response = await openAi.TextCompletion.Get("How long until we reach mars?");

            //if (response.IsSuccess)
            //{
            //    foreach(var result in response.Result.Choices)
            //    {
            //        Console.WriteLine(result.Text);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"{response.ErrorMessage}");
            //}
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }
    }
}