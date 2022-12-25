using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FilesService_Tests
{
    internal class FileService_GetAll : BaseServiceTest
    {
        const string responseJson = @"{
                                ""object"": ""list"",
                                ""data"": [
                                    {
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""purpose"": ""fine-tune"",
                                        ""filename"": ""@file.png"",
                                        ""bytes"": 207,
                                        ""created_at"": 1671818085,
                                        ""status"": ""processed"",
                                        ""status_details"": null
                                    },
                                    {
                                        ""object"": ""file"",
                                        ""id"": ""file-cFhMAt9eOwTjsmNgIcmJ4nxR"",
                                        ""purpose"": ""fine-tune"",
                                        ""filename"": ""@file.png"",
                                        ""bytes"": 207,
                                        ""created_at"": 1671820654,
                                        ""status"": ""processed"",
                                        ""status_details"": null
                                    }
                                ]
                            }
            ";



        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "GetAll_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetAll_When_Fail")]
        public async Task GetAll(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/files");

            var service = new FilesService(httpClient);
            var response = await service.Get();
        
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
