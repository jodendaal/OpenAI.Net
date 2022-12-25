using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ImagesService_Tests
{
    internal class ImageService_Edit : BaseServiceTest
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
        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "Edit_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "Edit_When_Fail")]
        public async Task Edit(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);
            var image = new Models.FileContentInfo(new byte[] { 1 }, "image.png");
            var request = new ImageEditRequest("A cute baby sea otter", image) { N = 2, Size = "1024x1024", Mask = new Models.FileContentInfo(new byte[] { 1 }, "image.png") };
            var response = await service.Edit(request);

       
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            Assert.NotNull(jsonRequest);
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
