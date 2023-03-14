using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Tests.Services.AudioService_Tests
{
    internal class AudioService_Translation : BaseServiceTest
    {
        const string responseJson = @"{""text"": ""Imagine the wildest idea that you've ever had, and you're curious about how it might scale to something that's a 100, a 1,000 times bigger. This is a place where you can get to do that.""}";
        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetTranslation_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request",TestName = "GetTranslation_When_Fail")]
        public async Task GetTranslation(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/audio/translations");

            var service = new AudioService(httpClient);
            var image = new Models.FileContentInfo(new byte[] { 1 }, "image.png");
            var request = new CreateTranslationRequest(image);
            var response = await service.GetTranslation(request);


            Assert.That(response.Result?.Text?.Contains("Imagine") ?? false, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetTranslationExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetTranslationExtension_When_Fail")]
        public async Task GetTranslationExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            Dictionary<string, string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors = new Dictionary<string, string>();
            expectedFormValues.Add("temperature", "2");

           
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/audio/translations", "https://api.openai.com",(request) => {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);
            });

            IAudioService service = new AudioService(httpClient);
            var response = await service.GetTranslation("Images/BabyCat.png",options =>{
                options.Temperature = 2;
            });

            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i => $"{i.Key}={i.Value}"))}");
            Assert.That(response.Result?.Text?.Contains("Imagine") ?? false, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
