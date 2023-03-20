using CodeGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;
using OpenAI.Net.Models.Responses;
using System.Text.Json;

namespace CodeGeneratorTest
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
                });
            })
            .Build();

            var openAi = host.Services.GetRequiredService<IOpenAIService>();
            var response = await openAi.Models.Get();

            if (response.IsSuccess)
            {
                var classText = ClassGenerator.GenerateModelsLookup(response.Result!);
                Console.WriteLine(classText);
                File.WriteAllText("ModelTypes.cs", classText);
            }
            else
            {
                Console.WriteLine($"{response.ErrorMessage}");
            }

            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();
        }
    }
}