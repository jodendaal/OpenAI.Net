using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    public class ChatCompletionService_Create : BaseTest
    {
        [TestCase(ModelTypes.GPT35Turbo, true, HttpStatusCode.OK, TestName = "Get_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "Get_When_Fail")]
        public async Task Get(string model, bool isSuccess, HttpStatusCode statusCode)
        {
            var messages = new List<Message>
            {
                Message.Create(ChatRoleType.User, "Say this is a test")
            };

            var request = new ChatCompletionRequest(messages)
            {
                Model = model
            };

            var response = await OpenAIService.Chat.Get(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (isSuccess)
            {
                Assert.That(response.Result!.Choices.FirstOrDefault()!.Message.Content, Is.EqualTo("\n\nThis is a test."), "Choices are not mapped correctly");
            }
        }

        [TestCase(ModelTypes.GPT35Turbo, true, HttpStatusCode.OK, TestName = "GetWithListExtension_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "GetWithListExtension_When_Fail")]
        public async Task GetWithListExtension(string model, bool isSuccess, HttpStatusCode statusCode)
        {
            var messages = new List<Message>
            {
                Message.Create(ChatRoleType.System, "You are a helpful assistant."),
                Message.Create(ChatRoleType.User, "Who won the world series in 2020?"),
                Message.Create(ChatRoleType.Assistant, "The Los Angeles Dodgers won the World Series in 2020."),
                Message.Create(ChatRoleType.User, "Where was it played?")
            };

            var response = await OpenAIService.Chat.Get(messages,o => {
                o.MaxTokens = 1000;
                o.Model = model;
            });

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (isSuccess)
            {
                Assert.That(response.Result?.Choices?.FirstOrDefault()?.Message.Content.Contains("Globe Life Field"), Is.EqualTo(true), "Incorrect answer");
            }
        }
    }
}
