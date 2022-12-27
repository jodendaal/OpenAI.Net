using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ImagesService_Tests
{
    internal class ImageService_Generate : BaseServiceTest
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

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Generate_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Generate_When_Fail")]
        public async Task Generate(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/generations", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);

            var request = new ImageGenerationRequest("A cute baby sea otter") { N = 2, Size = "1024x1024" };
            var response = await service.Generate(request);
           
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
           
            Assert.NotNull(jsonRequest);

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GenerateWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GenerateWithExtension_When_Fail")]
        public async Task GenerateWithExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/generations", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);

            var response = await service.Generate("A cute baby sea otter",2, "1024x1024", o => {
                o.User = "test";
            });

            Assert.That(jsonRequest.Contains(@"""user"":""test"""));
            Assert.That(jsonRequest.Contains(@"""size"":""1024x1024"""));
            Assert.That(jsonRequest.Contains(@"""n"":2"));
            Assert.That(jsonRequest.Contains(@"""prompt"":""A cute baby sea otter"""));

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));

            Assert.NotNull(jsonRequest);

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GenerateWithExtensionOptionOnly_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GenerateWithExtensionOptionOnly_When_Fail")]
        public async Task GenerateWithExtensionOptionOnly(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/generations", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);

            var response = await service.Generate("A cute baby sea otter", o => {
                o.User = "test";
                o.N = 2;
                o.Size = "1024x1024";
            });

            Assert.That(jsonRequest.Contains(@"""user"":""test"""));
            Assert.That(jsonRequest.Contains(@"""size"":""1024x1024"""));
            Assert.That(jsonRequest.Contains(@"""n"":2"));
            Assert.That(jsonRequest.Contains(@"""prompt"":""A cute baby sea otter"""));

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));

            Assert.NotNull(jsonRequest);

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
