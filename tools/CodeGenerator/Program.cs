using CodeGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;

namespace CodeGeneratorTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(con => {
                con.AddUserSecrets<Program>();
            })
            .ConfigureServices((builder, services) =>
            {
                services.AddOpenAIServices(options => {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                });
            })
            .Build();

            var openAi = host.Services.GetService<IOpenAIService>();
            var response = await openAi.Models.Get();

            if (response.IsSuccess)
            {
                var classText = ClassGenerator.GenerateModelsLookup(response.Result);
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