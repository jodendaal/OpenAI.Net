using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class ModelsService : BaseTest
    {
        [TestCase("text-davinci-edit-001",true, HttpStatusCode.OK,TestName = "GetById_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,TestName = "GetById_When_Invalid_Model_Fail")]
        public async Task GetById(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            

            var response = await OpenAIService.Models.Get(model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Id == "text-davinci-edit-001", Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }

        [TestCase(true, HttpStatusCode.OK)]
        public async Task GetAll(bool isSuccess, HttpStatusCode statusCode)
        {
            

            var response = await OpenAIService.Models.Get();

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data.Count() > 0, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }
    }
}
