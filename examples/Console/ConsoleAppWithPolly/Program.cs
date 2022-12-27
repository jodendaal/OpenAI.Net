using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;
using Polly.Extensions.Http;
using Polly;

namespace ConsoleAppWithPolly
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddOpenAIServices(options => {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                }, 
                (httpClientOptions) => {
                    httpClientOptions.AddPolicyHandler(GetRetryPolicy());
                });
            })
            .Build();

            var openAi = host.Services.GetService<IOpenAIService>();
            var response = await openAi.TextCompletion.Get("How long until we reach mars?");

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

            static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
            {
                return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                                retryAttempt)));
            }
        }
    }
}