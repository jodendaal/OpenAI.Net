using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ChatCompletionService_Tests
{
    internal class ChatCompletionService_Create : BaseServiceTest
    {
        const string responseJson = @"{""id"":""chatcmpl-6tjUTDUidlAZv7bkF9kZjXPmj1ECZ"",
                                    ""object"":""chat.completion"",""created"":1678740925,
                                    ""model"":""gpt-3.5-turbo-0301"",
                                    ""usage"":{""prompt_tokens"":12,""completion_tokens"":7,""total_tokens"":19},
                                    ""choices"":[{""message"":{""role"":""assistant"",""content"":""\n\nThis is a test.""},
                                    ""finish_reason"":""stop"",""index"":0}]}
                                    ";

        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Create_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Create_When_Fail")]
        public async Task Create(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com",(request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = new Message(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(message);
            var response = await service.Create(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "CreateList_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "CreateList_When_Fail")]
        public async Task CreateList(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = new Message(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(message.ToList());
            var response = await service.Create(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "CreateWithModel_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "CreateWithModel_When_Fail")]
        public async Task CreateWithModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = new Message(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(ModelTypes.GPT35Turbo, message);
            var response = await service.Create(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }
    }
}
