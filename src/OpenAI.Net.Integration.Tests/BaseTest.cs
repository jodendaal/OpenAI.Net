using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Integration.Tests
{
    public class BaseTest
    {
        public TestConfig Config { get; private set; }
        public static HttpClient? HttpClient { get; private set; }
        public BaseTest()
        {
            var configuration = new ConfigurationBuilder()
            .AddUserSecrets<BaseTest>()
            .Build();

            Config = new TestConfig(configuration);

            if(HttpClient == null) 
            {
                HttpClient = new HttpClient();
                HttpClient.BaseAddress = new Uri(Config.ApiUrl);
                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Config.Apikey);
            }
        }
    }

    public class TestConfig
    {
        public TestConfig(IConfigurationRoot configuration)
        {
            Apikey = configuration["Apikey"];
            ApiUrl = configuration["ApiUrl"];
        }

        public string Apikey { get; private set; }
        public string ApiUrl { get; private set; }
    }
}
