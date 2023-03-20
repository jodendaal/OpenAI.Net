using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    public class TextCompletionService_Get : BaseTest
    {
        [TestCase(ModelTypes.TextDavinci003, true, HttpStatusCode.OK,null, TestName = "Get_When_Success")]
        [TestCase(ModelTypes.TextDavinci003, true, HttpStatusCode.OK, true, TestName = "Get_When_Echo_True_Success")]
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
            Assert.That(response.Result?.Choices?.Length == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (echo.HasValue && isSuccess)
            {
                var containsPrompt = response.Result.Choices[0].Text.Contains("Say this is a test");
                Assert.That(containsPrompt, Is.True, "Prompt not returned when Echo was true");
            }
            else if(isSuccess)
            {
                Assert.That(request.Echo, Is.EqualTo(null), "Echo default should be null/not set");
            }
        }

        [TestCase(ModelTypes.TextDavinci003, true, HttpStatusCode.OK, null, TestName = "GetExtension_When_Success")]
        [TestCase(ModelTypes.TextDavinci003, true, HttpStatusCode.OK, true, TestName = "GetExtension_When_Echo_True_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, false, TestName = "GetExtension_When_Fail")]
        public async Task GetExtension(string model, bool isSuccess, HttpStatusCode statusCode, bool? echo)
        {
            var request = new TextCompletionRequest(model, "Say this is a test");


            var logitBias = new Dictionary<string, int>
            {
                { "50256", -100 }
            };
            var response = await OpenAIService.TextCompletion.Get(model, "Say this is a test",(o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
                o.LogitBias = logitBias;
                o.Echo = echo ?? false;
            });
          


            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Length == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            if (echo.HasValue && isSuccess)
            {
                Assert.That(response.Result.Choices[0].Text.Contains("Say this is a test"), Is.EqualTo(echo), "Prompt not returned when Echo was true");
            }
            else if (isSuccess)
            {
                Assert.That(request.Echo, Is.EqualTo(null), "Echo default should be null/not set");
            }
        }


        [TestCase(ModelTypes.TextDavinci003, true, HttpStatusCode.OK,  TestName = "GetList_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,  TestName = "GetList_When_Fail")]
        public async Task GetListExtension(string model, bool isSuccess, HttpStatusCode statusCode)
        {
            var request = new TextCompletionRequest(model, "Say this is a test");

            var prompts = new List<string>()
            {
                "Say this is a test",
                "Say this is not a test"
            };

            var response = await OpenAIService.TextCompletion.Get(model, prompts, (o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
            });
            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
                Assert.That(response.StatusCode, Is.EqualTo(statusCode));
                Assert.That(response?.Result?.Choices?.Length == 2, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            });
        }
    }
}
