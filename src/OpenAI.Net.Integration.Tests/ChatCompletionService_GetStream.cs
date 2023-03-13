using OpenAI.Net.Models.Requests;

namespace OpenAI.Net.Integration.Tests
{
    public class ChatCompletionService_GetStream : BaseTest
    {
        [Test]
        public async Task CreateStream()
        {
            var prompt = @"Say this is a test";
            var message = new Message(ChatRoleType.User, prompt);
            var request = new ChatCompletionRequest(message);

            await foreach(var t in OpenAIService.Chat.CreateStream(request))
            {
                Console.WriteLine(t?.Result?.Choices[0].Delta?.Content);
                Assert.True(t.IsSuccess, "Failed to get chat stream", t.ErrorMessage);
            }
        }
    }
}
