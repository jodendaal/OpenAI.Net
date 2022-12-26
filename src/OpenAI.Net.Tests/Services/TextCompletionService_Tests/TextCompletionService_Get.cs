using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.TextCompletionService_Tests
{
    internal class TextCompletionService_Get : BaseServiceTest
    {
        const string responseJson = @"{
                ""id"": ""cmpl-6PtAJQgmP51aSZDoG1PFuorDwP9aZ"",
                ""object"": ""text_completion"",
                ""created"": 1671628275,
                ""model"": ""text-davinci-003"",
                ""choices"": [
                    {
                        ""text"": ""\n\nThis is indeed a test"",
                        ""index"": 0,
                        ""logprobs"": null,
                        ""finish_reason"": ""length""
                    }
                ],
                ""usage"": {
                    ""prompt_tokens"": 5,
                    ""completion_tokens"": 7,
                    ""total_tokens"": 12
                }
            }";

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Get_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Get_When_Fail")]
        public async Task Get(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com",(request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var request = new TextCompletionRequest("text-davinci-003", "Say this is a test");
            var response = await service.Get(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetList_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetList_When_Fail")]
        public async Task GetList(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var request = new TextCompletionRequest("text-davinci-003", new List<string>() { "Say this is a test" });
            var response = await service.Get(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetExtensionWithOptions_When_Fail")]
        public async Task GetExtensionWithOptions(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var request = new TextCompletionRequest("text-davinci-003", "Say this is a test");
            var response = await service.Get("text-davinci-003", "Say this is a test", (o) => {
                o.Echo = true;
                o.LogProbs = 99;
            });
            
            Assert.That(jsonRequest.Contains(@"""logprobs"":99"));
            Assert.That(jsonRequest.Contains(@"""echo"":true"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetListExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetListExtensionWithOptions_When_Fail")]
        public async Task GetListExtensionWithOptions(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var response = await service.Get("text-davinci-003", new List<string>() { "Say this is a test"}, (o) => {
                o.Echo = true;
                o.LogProbs = 99;
            });

            Assert.That(jsonRequest.Contains(@"""logprobs"":99"));
            Assert.That(jsonRequest.Contains(@"""echo"":true"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetExtensionWithOptions_When_Fail")]
        public async Task GetExtensionWithOptionsAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var response = await service.Get("Say this is a test", (o) => {
                o.Echo = true;
                o.LogProbs = 99;
            });

            Assert.That(jsonRequest.Contains(@"""logprobs"":99"));
            Assert.That(jsonRequest.Contains(@"""echo"":true"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetListExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetListExtensionWithOptions_When_Fail")]
        public async Task GetListExtensionWithOptionsAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);
            var response = await service.Get(new List<string>() { "Say this is a test" }, (o) => {
                o.Echo = true;
                o.LogProbs = 99;
            });

            Assert.That(jsonRequest.Contains(@"""logprobs"":99"));
            Assert.That(jsonRequest.Contains(@"""echo"":true"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }
    }
}
