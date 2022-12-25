using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Extensions;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    internal class FileService_GetContent : BaseServiceTest
    {
        const string responseJson = @"
                                    {
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""purpose"": ""fine-tune"",
                                        ""filename"": ""@file.png"",
                                        ""bytes"": 207,
                                        ""created_at"": 1671818085,
                                        ""status"": ""processed"",
                                        ""status_details"": null
                                    }";



        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "GetContent_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetContent_When_Fail")]
        public async Task GetContent(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var imageEditRequest = new ImageEditRequest("a baby fish", new Models.FileContentInfo(new byte[] { 1 }, "image.png"));
            var formDataContent = imageEditRequest.ToMultipartFormDataContent();
            formDataContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            formDataContent.Headers.ContentDisposition.FileName = "image.png";

            var jsonContent = new StringContent(responseJson);

            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = isSuccess ? formDataContent : jsonContent };

            var httpClient = GetHttpClient(responseStatusCode, res, "/v1/files/1/content");

            var service = new FilesService(httpClient);
            var response = await service.GetContent("1");

         
            Assert.That(response.Result?.FileContent.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.FileName == "image.png", Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
