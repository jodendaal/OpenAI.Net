using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

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

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "UploadWithExtensionFilePath_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request", TestName = "UploadWithExtensionFilePath_When_Fail")]
        public async Task UploadWithExtensionFilePath(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files");

            var service = new FilesService(httpClient);
            var response = await service.Upload(@"Images\BabyCat.png");

            Assert.That(response.Result?.Bytes == 207, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "UploadWithExtensionFilePathAndName_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request", TestName = "UploadWithExtensionFilePathAndName_When_Fail")]
        [TestCase(false, HttpStatusCode.OK, responseJson, null, "invalid_path", TestName = "UploadWithExtensionFilePathAndName_When_Path_Invalid")]
        public async Task UploadWithExtensionFilePathAndName(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage, string filePath = @"Images\BabyCat.png")
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files");
            bool exceptionRaised = false;
            OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse> response = null;
            try
            {
                var service = new FilesService(httpClient);
                response = await service.UploadWithName(filePath, "mymode.jsonl");
            }
            catch(FileNotFoundException)
            {
                exceptionRaised = true;
            }

            Assert.That((filePath == "invalid_path" && exceptionRaised) || filePath != "invalid_path");
            if (exceptionRaised)
            {
                return;
            }
            Assert.That(response?.Result?.Bytes == 207, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "UploadWithExtensionFileBytesAndName_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "UploadWithExtensionFileBytesAndName_When_Fail")]
        
        public async Task UploadWithExtensionFileBytesAndName(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage,string filePath = @"Images\BabyCat.png")
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files");

            var service = new FilesService(httpClient);
            var bytes = File.ReadAllBytes(filePath);
            var response = await service.Upload(bytes, "mymode.jsonl");

            Assert.That(response.Result?.Bytes == 207, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
