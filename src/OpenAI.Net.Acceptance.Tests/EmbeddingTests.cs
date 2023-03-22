using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class EmbeddingTests : BaseTest
    {
        [Test]
        public async Task Create()
        {
            var request = CreateObjectWithRandomData<EmbeddingsRequest>();
            var responseObject = CreateObjectWithRandomData<EmbeddingsResponse>();

            ConfigureWireMockPostJson("/v1/embeddings", request,responseObject);

            var response = await OpenAIService.Embeddings.Create(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }
    }
}
