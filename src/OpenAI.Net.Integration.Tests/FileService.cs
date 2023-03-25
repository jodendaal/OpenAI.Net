using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class FileService : BaseTest
    {
        [Test]
        public async Task GetAll()
        {
           var response = await OpenAIService.Files.Get();

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Upload()
        {
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await OpenAIService.Files.Upload(new FileUploadRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete()
        {
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await OpenAIService.Files.Upload(new FileUploadRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), $"Request failed {response.ErrorMessage}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            await Task.Delay(5000);
            var deleteResponse = await OpenAIService.Files.Delete(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), $"Request failed {response.ErrorMessage}");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetById()
        {
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await OpenAIService.Files.Upload(new FileUploadRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            await Task.Delay(2000);

            var retrieveFileResponse = await OpenAIService.Files.Get(response?.Result?.Id);
            Assert.That(retrieveFileResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(retrieveFileResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Result.Id, Is.EqualTo(retrieveFileResponse?.Result?.Id));

            await Task.Delay(2000);
            var deleteResponse = await OpenAIService.Files.Delete(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deleteResponse.Result?.Deleted, Is.EqualTo(true));
        }

        [Test]
        public async Task Remove()
        {
            
            var file = FileContentInfo.Load(@"Data\trainingData.jsonl");
            var response = await OpenAIService.Files.Upload(new FileUploadRequest(file, "fine-tune"));

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            await Task.Delay(5000);

            var retrieveFileResponse = await OpenAIService.Files.GetContent(response?.Result?.Id);
            Assert.That(retrieveFileResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(retrieveFileResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            await Task.Delay(5000);

            var deleteResponse = await OpenAIService.Files.Delete(response?.Result?.Id);

            Assert.That(deleteResponse.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
