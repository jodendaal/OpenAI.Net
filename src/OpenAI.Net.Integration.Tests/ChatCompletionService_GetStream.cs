using OpenAI.Net.Models.Requests;

namespace OpenAI.Net.Integration.Tests
{
    public class ChatCompletionService_GetStream : BaseTest
    {
        [Test]
        public async Task GetStream()
        {
            var prompt = @"Say this is a test";
            var message = Message.Create(ChatRoleType.User, prompt);
            var request = new ChatCompletionRequest(message);

            await foreach(var response in OpenAIService.Chat.GetStream(request))
            {
                Console.WriteLine(response?.Result?.Choices[0].Delta?.Content);
                Assert.True(response?.IsSuccess, "Failed to get chat stream", response?.ErrorMessage);
            }
        }

        [Test]
        public async Task GetStreamWithListExtension()
        {
            var messages = new List<Message>
            {
                Message.Create(ChatRoleType.System, "You are a helpful assistant."),
                Message.Create(ChatRoleType.User, "Who won the world series in 2020?"),
                Message.Create(ChatRoleType.Assistant, "The Los Angeles Dodgers won the World Series in 2020."),
                Message.Create(ChatRoleType.User, "Where was it played?")
            };

            await foreach (var response in OpenAIService.Chat.GetStream(messages))
            {
                Console.WriteLine(response?.Result?.Choices[0].Delta?.Content);
                Assert.True(response?.IsSuccess, "Failed to get chat stream", response?.ErrorMessage);
            }
        }
    }
}
