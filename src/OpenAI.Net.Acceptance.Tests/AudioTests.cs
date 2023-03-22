using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class AudioTests : BaseTest
    {
        [Test]
        public async Task GetTranscription()
        {
            var request = CreateObjectWithRandomData<CreateTranscriptionRequest>();
            var responseObject = CreateObjectWithRandomData<AudioReponse>();

            ConfigureWireMockPostForm("/v1/audio/transcriptions", request, responseObject);

            var response = await OpenAIService.Audio.GetTranscription(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Text.Should().NotBeNullOrEmpty();
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task GetTranslation()
        {
            var request = CreateObjectWithRandomData<CreateTranslationRequest>();
            var responseObject = CreateObjectWithRandomData<AudioReponse>();

            ConfigureWireMockPostForm("/v1/audio/translations", request, responseObject);

            var response = await OpenAIService.Audio.GetTranslation(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Text.Should().NotBeNullOrEmpty();
            response.Result.Should().BeEquivalentTo(responseObject);
        }


    }
}
