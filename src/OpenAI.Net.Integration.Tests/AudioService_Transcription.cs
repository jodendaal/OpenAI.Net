using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class AudioService_Transcription : BaseTest
    {
        [TestCase(ModelTypes.Whisper1,true, HttpStatusCode.OK ,TestName = "GetTranscription_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "GetTranscription_When_Fail")]
        public async Task GetTranscription(string model,bool isSuccess, HttpStatusCode statusCode)
        {

            var request = new CreateTranscriptionRequest(FileContentInfo.Load(@"Audio\TestTranscription.m4a"));
            request.Model = model;

            var response = await OpenAIService.Audio.GetTranscription(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Text?.ToLowerInvariant().Contains("testing")??false, Is.EqualTo(isSuccess), "Should contain the word Testing");
            Assert.That((response.Result?.Text?.Contains("1") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("one") ?? false), Is.EqualTo(isSuccess), "Should contain the word 1 or one");
            Assert.That((response.Result?.Text?.Contains("2") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("two") ?? false), Is.EqualTo(isSuccess), "Should contain the word 2 or two");
            Assert.That((response.Result?.Text?.Contains("3") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("three") ?? false), Is.EqualTo(isSuccess), "Should contain the word 3 or three");
        }

        [TestCase(ModelTypes.Whisper1, true, HttpStatusCode.OK, TestName = "GetTranscriptionWithExtension_When_Success")]
        public async Task GetTranscriptionWithExtension(string model, bool isSuccess, HttpStatusCode statusCode)
        {

            var response = await OpenAIService.Audio.GetTranscription(@"Audio\TestTranscription.m4a");

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Text?.ToLowerInvariant().Contains("testing") ?? false, Is.EqualTo(isSuccess), "Should contain the word Testing");
            Assert.That((response.Result?.Text?.Contains("1") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("one") ?? false), Is.EqualTo(isSuccess), "Should contain the word 1 or one");
            Assert.That((response.Result?.Text?.Contains("2") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("two") ?? false), Is.EqualTo(isSuccess), "Should contain the word 2 or two");
            Assert.That((response.Result?.Text?.Contains("3") ?? false) || (response.Result?.Text?.ToLowerInvariant().Contains("three") ?? false), Is.EqualTo(isSuccess), "Should contain the word 3 or three");
        }

    }
}
