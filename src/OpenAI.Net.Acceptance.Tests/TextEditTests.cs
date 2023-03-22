using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class TextEditTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            
            var textCompletionRequest = CreateObjectWithRandomData<TextEditRequest>();
            var textCompletionResponse = CreateObjectWithRandomData<TextEditResponse>();

            ConfigureWireMockPostJson("/v1/edits", textCompletionRequest, textCompletionResponse);

            var response = await OpenAIService.TextEdit.Get(textCompletionRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(textCompletionResponse);
        }
    }
}
