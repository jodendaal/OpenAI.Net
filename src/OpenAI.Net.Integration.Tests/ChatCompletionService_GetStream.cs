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

            await foreach(var t in OpenAIService.Chat.GetStream(request))
            {
                Console.WriteLine(t?.Result?.Choices[0].Delta?.Content);
                Assert.True(t.IsSuccess, "Failed to get chat stream", t.ErrorMessage);
            }
        }
    }
}
