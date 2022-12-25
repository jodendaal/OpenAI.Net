using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    internal class FileService_Upload : BaseServiceTest
    {
        const string responseJson = @"{
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""purpose"": ""fine-tune"",
                                        ""filename"": ""@file.png"",
                                        ""bytes"": 207,
                                        ""created_at"": 1671818085,
                                        ""status"": ""processed"",
                                        ""status_details"": null
                                    }
            ";

        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "Upload_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request", TestName = "Upload_When_Fail")]
        public async Task Upload(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files");

            var service = new FilesService(httpClient);
            var image = new Models.FileContentInfo(new byte[] { 1 }, "image.png");
            var request = new FileUploadRequest(image);
            var response = await service.Upload(request);

           
            Assert.That(response.Result?.Bytes == 207, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
