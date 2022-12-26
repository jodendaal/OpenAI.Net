using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    public class TextCompletionService_Get : BaseTest
    {
        [TestCase("text-davinci-003", true, HttpStatusCode.OK,null, TestName = "Get_When_Success")]
        [TestCase("text-davinci-003", true, HttpStatusCode.OK, true, TestName = "Get_When_Echo_True_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,false, TestName = "Get_When_Fail")]
        public async Task Get(string model,bool isSuccess, HttpStatusCode statusCode, bool? echo)
        {
            var request = new TextCompletionRequest(model, "Say this is a test");
           
            if (echo.HasValue)
            {
                request.Echo = echo.Value;
            }

            var response = await OpenAIService.TextCompletion.Get(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (echo.HasValue && isSuccess)
            {
                Assert.That(response.Result.Choices[0].Text.Contains("Say this is a test"), Is.EqualTo(echo), "Prompt not returned when Echo was true");
            }
            else if(isSuccess)
            {
                Assert.That(request.Echo, Is.EqualTo(null), "Echo default should be null/not set");
            }
        }

        [TestCase("text-davinci-003", true, HttpStatusCode.OK, null, TestName = "Get_When_Success")]
        [TestCase("text-davinci-003", true, HttpStatusCode.OK, true, TestName = "Get_When_Echo_True_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, false, TestName = "Get_When_Fail")]
        public async Task GetExtension(string model, bool isSuccess, HttpStatusCode statusCode, bool? echo)
        {
            var request = new TextCompletionRequest(model, "Say this is a test");

            if (echo.HasValue)
            {
                request.Echo = echo.Value;
            }

            var response = await OpenAIService.TextCompletion.Get(model, "Say this is a test",(o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
            });
          


            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (echo.HasValue && isSuccess)
            {
                Assert.That(response.Result.Choices[0].Text.Contains("Say this is a test"), Is.EqualTo(echo), "Prompt not returned when Echo was true");
            }
            else if (isSuccess)
            {
                Assert.That(request.Echo, Is.EqualTo(null), "Echo default should be null/not set");
            }
        }
    }
}
