using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class ModerationTests : BaseTest
    {
        [Test]
        public async Task Create()
        {
            var request = CreateObjectWithRandomData<ModerationRequest>();
            var responseObject = CreateObjectWithRandomData<ModerationResponse>();

            ConfigureWireMockPostJson("/v1/moderations", request, responseObject);

            var response = await OpenAIService.Moderation.Create(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Results.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }
    }
}
