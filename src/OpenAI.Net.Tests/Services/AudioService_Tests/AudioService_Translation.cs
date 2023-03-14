using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

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
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            string jsonRequest = null;
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/audio/translations", "https://api.openai.com",(request) => {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new AudioService(httpClient);

            var request = new CreateTranslationRequest("Test.mp3");
            var response = await service.GetTranslation(request);

         
            Assert.AreEqual(!string.IsNullOrEmpty(response.Result?.Text), isSuccess);
            Assert.That(jsonRequest.Contains("file"), Is.EqualTo(true), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
