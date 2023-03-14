using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ChatCompletionService_Tests
{
    internal class ChatCompletionService_Get : BaseServiceTest
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

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Get_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Get_When_Fail")]
        public async Task Get(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = Message.Create(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(message);
            var response = await service.Get(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetList_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetList_When_Fail")]
        public async Task GetList(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = Message.Create(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(message.ToList());
            var response = await service.Get(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetWithMessageListExtrension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetWithMessageListExtrension_When_Fail")]
        public async Task GetWithMessageListExtrension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var messages = new List<Message> { Message.Create(ChatRoleType.User, "Say this is a test") };

            var response = await service.Get(messages, options => {
                options.TopP = 10;
            });

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            Assert.That(jsonRequest.Contains(@"""top_p"":10", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Options where not applied for TopP");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetWithMessageExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetWithMessageExtension_When_Fail")]
        public async Task GetWithMessageExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var messages= Message.Create(ChatRoleType.User, "Say this is a test");

            var response = await service.Get(messages, options => {
                options.TopP = 10;
            });

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            Assert.That(jsonRequest.Contains(@"""top_p"":10", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Options where not applied for TopP");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetWithUserMessageExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetWithUserMessageExtension_When_Fail")]
        public async Task GetWithUserMessageExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);

            var response = await service.Get("Say this is a test",options => {
                options.TopP = 10;
            });

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            Assert.That(jsonRequest.Contains(@"""top_p"":10", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Options where not applied for TopP");
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetWithModel_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetWithModel_When_Fail")]
        public async Task GetWithModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) =>
            {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);
            var message = Message.Create(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(ModelTypes.GPT35Turbo, message);
            var response = await service.Get(request);

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
        }


        [TestCase(true, ChatRoleType.User, TestName = "CreateMessage_When_Success_User")]
        [TestCase(true, ChatRoleType.System, TestName = "CreateMessage_When_Success_System")]
        [TestCase(true, ChatRoleType.Assistant, TestName = "CreateMessage_When_Success_Assistant")]
        [TestCase(false, "unknown", TestName = "CreateMessage_When_Fail_UnknownRole")]
        public async Task CreateMessage(bool isSuccess, string role)
        {
            if (isSuccess)
            {
                Assert.DoesNotThrow(() =>
                {
                    Message.Create(role, "Test");
                });
            }
            else
            {
                var exception = Assert.Throws<ArgumentException>(() =>
                {
                    Message.Create(role, "Test");
                });
                var validTypes = new string[] { ChatRoleType.User, ChatRoleType.System, ChatRoleType.Assistant };
                Assert.That($"Role must be one of the following ${string.Join(",", validTypes)} (Parameter 'role')", Is.EqualTo(exception.Message));
            }
        }
    }
}
