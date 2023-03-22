using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class AudioService_Translations : BaseTest
    {
        [TestCase(ModelTypes.Whisper1,true, HttpStatusCode.OK ,TestName = "GetTranslation_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "GetTranslation_When_Fail")]
        public async Task GetTranslation(string model,bool isSuccess, HttpStatusCode statusCode)
        {

            var request = new CreateTranslationRequest(FileContentInfo.Load(@"Audio\Translation.m4a"))
            {
                Model = model
            };

            var response = await OpenAIService.Audio.GetTranslation(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Text?.Contains("My name is Tim") ?? false, Is.EqualTo(isSuccess), "Should contain the words My name is Tim");
            Assert.That(response.Result?.Text?.Contains("Programming") ?? false, Is.EqualTo(isSuccess), "Should contain the word Programming");
        }

        [TestCase(true, HttpStatusCode.OK, TestName = "GetTranslationWithExtension_When_Success")]
        public async Task GetTranslationWithExtension(bool isSuccess, HttpStatusCode statusCode)
        {

            var response = await OpenAIService.Audio.GetTranslation(@"Audio\Translation.m4a");

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Text?.Contains("My name is Tim") ?? false, Is.EqualTo(isSuccess), "Should contain the words My name is Tim");
            Assert.That(response.Result?.Text?.Contains("Programming") ?? false, Is.EqualTo(isSuccess), "Should contain the word Programming");
        }

    }


}
