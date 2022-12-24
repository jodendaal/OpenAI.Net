using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class GetModelsTests : BaseTest
    {
        [TestCase("text-davinci-edit-001",true, HttpStatusCode.OK ,TestName = "Successfull Request")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "Failed Request")]
        public async Task Test_GetModelTests(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);

            var response = await openAIHttpClient.GetModel(model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Id == "text-davinci-edit-001", Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }

        [TestCase(true, HttpStatusCode.OK, TestName = "GetModels Successfull Request")]
        public async Task Test_GetModelsTests(bool isSuccess, HttpStatusCode statusCode)
        {
            var openAIHttpClient = new OpenAIHttpClient(HttpClient);

            var response = await openAIHttpClient.GetModels();

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data.Count() > 0, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }
    }
}
