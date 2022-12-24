using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class TextEditService : BaseTest
    {
        [TestCase("text-davinci-edit-001",true, HttpStatusCode.OK ,TestName = "Get_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "Get_When_Fail")]
        public async Task Get(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var request = new TextEditRequest(model, "Fix the spelling mistakes", "What day of the wek is it?");

            var response = await OpenAIService.TextEdit.Get(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            Assert.That(response.Result?.Choices?[0].Text?.Contains("What day of the week is it"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
        }
    }
}
