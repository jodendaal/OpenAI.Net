using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Acceptance.Tests
{
    public class FileTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            var responseObject = CreateObjectWithRandomData<FileListResponse>();

            ConfigureWireMockGet("/v1/files", responseObject);

            var response = await OpenAIService.Files.Get();

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task GetById()
        {
            var responseObject = CreateObjectWithRandomData<FileInfoResponse>();
            var fileId = "1";
            ConfigureWireMockGet($"/v1/files/{fileId}", responseObject);

            var response = await OpenAIService.Files.Get(fileId);

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        //[Test]//QQQ
        //public async Task GetContent()
        //{
        //    var responseObject = CreateObjectWithRandomData<FileContentInfo>();
        //    var fileId = "1";
        //    ConfigureWireMockGet($"/v1/files/{fileId}/content", responseObject);

        //    var response = await OpenAIService.Files.GetContent(fileId);

        //    response.IsSuccess.Should().BeTrue();
        //    response.Result.Should().BeEquivalentTo(responseObject);
        //}


        [Test]
        public async Task Delete()
        {
            var responseObject = CreateObjectWithRandomData<DeleteResponse>();

            var fileId = "1";
            ConfigureWireMockDelete($"/v1/files/{fileId}", responseObject);

            var response = await OpenAIService.Files.Delete(fileId);

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]//QQQ
        public async Task Upload()
        {
            var textCompletionRequest = CreateObjectWithRandomData<FileUploadRequest>();
            var textCompletionResponse = CreateObjectWithRandomData<FileInfoResponse>();

            ConfigureWireMockPostForm("/v1/files", textCompletionRequest, textCompletionResponse);

            var response = await OpenAIService.Files.Upload(textCompletionRequest);

            response.IsSuccess.Should().BeTrue();
           // response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(textCompletionResponse);
        }
    }
}
