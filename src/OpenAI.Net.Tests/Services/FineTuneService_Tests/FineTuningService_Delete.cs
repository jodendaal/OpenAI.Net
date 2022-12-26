using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class FineTuningService_Delete : BaseServiceTest
    {
        const string responseJson = @"{
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""deleted"":true
                                    }
            ";



        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "Delete_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Delete_When_Fail")]
        public async Task Delete(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/models/modelId");

            var service = new FineTuneService(httpClient);
            var response = await service.Delete("modelId");

           
            Assert.That(response.Result?.Deleted == true, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Deleted == true, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
