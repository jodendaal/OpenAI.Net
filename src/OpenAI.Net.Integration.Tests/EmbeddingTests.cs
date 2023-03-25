using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;

namespace OpenAI.Net.Integration.Tests
{
    public class EmbeddingService: BaseTest
    {
        [Test]
        public async Task Create()
        {
            var request = new EmbeddingsRequest("this is a test", OpenAIDefaults.EmbeddingsModel);
            var response = await OpenAIService.Embeddings.Create(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Result?.Data?.Count(), Is.GreaterThanOrEqualTo(1));
        }
    }
}
