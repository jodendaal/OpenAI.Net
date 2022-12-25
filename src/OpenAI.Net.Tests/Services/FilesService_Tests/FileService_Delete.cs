using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    internal class FileService_Delete : BaseServiceTest
    {
        const string responseJson = @"{
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""deleted"":true
                                    }
            ";



        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName ="Delete_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "Delete_When_Fail")]
        public async Task Delete(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files/1");

            var service = new FilesService(httpClient);
            var response = await service.Delete("1");

           
            Assert.That(response.Result?.Deleted == true, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Deleted == true, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
