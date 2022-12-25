using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ImagesService_Tests
{
    internal class ImageService_Variation : BaseServiceTest
    {
        const string responseJson = @"{
              ""created"": 1589478378,
              ""data"": [
                {
                  ""url"": ""https://...""
                },
                {
                  ""url"": ""https://...""
                }
              ]
            }
            ";
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Variation_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request",TestName = "Variation_When_Fail")]
        public async Task Variation(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/variations", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);
            var image = new byte[] { 1 };
            var request = new ImageVariationRequest(new Models.FileContentInfo(new byte[] { 1 }, "image.png")) { N = 2, Size = "1024x1024" };
            var response = await service.Variation(request);

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            Assert.NotNull(jsonRequest);
            AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
        }
    }
}
