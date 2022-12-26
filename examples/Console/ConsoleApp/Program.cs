using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;

namespace ConsoleApp
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddOpenAIServices(options => {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                });
            })
            .Build();

            var openAi = host.Services.GetService<IOpenAIService>();
            var response = await openAi.TextCompletion.Get("How long until we reach mars?");

            if (response.IsSuccess)
            {
                foreach(var result in response.Result.Choices)
                {
                    Console.WriteLine(result.Text);
                }
            }
            else
            {
                Console.WriteLine($"{response.ErrorMessage}");
            }
        }
    }
}