using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    [TestFixture(TestName = "FileService")]
    internal class FileService_Upload
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
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            string path = null;
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res)
               .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
               {
                   path = r.RequestUri.AbsolutePath;
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };

            var service = new FilesService(httpClient);
            var image = new Models.FileContentInfo(new byte[] { 1 }, "image.png");
            var request = new UploadFileRequest(image);
            var response = await service.Upload(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess));
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Bytes == 207, Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.That(path, Is.EqualTo("/v1/files"), "Apth is incorrect");
        }
    }
}
