using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class FilesTests : BaseTest
    {
        [Test]
        public async Task Test_ListFiles()
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);
           var response = await openAIHttpClient.GetFiles();

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Test_UploadFile()
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await openAIHttpClient.UploadFile(new UploadFileRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Test_DeleteFile()
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await openAIHttpClient.UploadFile(new UploadFileRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), $"Request failed {response.ErrorMessage}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var deleteResponse = await openAIHttpClient.DeleteFile(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), $"Request failed {response.ErrorMessage}");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Test_RetrieveFile()
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await openAIHttpClient.UploadFile(new UploadFileRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var retrieveFileResponse = await openAIHttpClient.RetrieveFile(response?.Result?.Id);
            Assert.That(retrieveFileResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(retrieveFileResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Result.Id, Is.EqualTo(retrieveFileResponse?.Result?.Id));

            var deleteResponse = await openAIHttpClient.DeleteFile(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Test_RetrieveFileContent()
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await openAIHttpClient.UploadFile(new UploadFileRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            await Task.Delay(1000);

            var retrieveFileResponse = await openAIHttpClient.RetrieveFileContent(response?.Result?.Id);
            Assert.That(retrieveFileResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(retrieveFileResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            await Task.Delay(1000);

            var deleteResponse = await openAIHttpClient.DeleteFile(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
