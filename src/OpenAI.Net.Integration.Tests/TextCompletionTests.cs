using OpenAI.Net.Models.Requests;
using System.Net;
using System.Reflection;

namespace OpenAI.Net.Integration.Tests
{
    public class TextCompletionTests : BaseTest
    {
        [TestCase("text-davinci-003", true, HttpStatusCode.OK,null, TestName = "Successfull Request")]
        [TestCase("text-davinci-003", true, HttpStatusCode.OK, true, TestName = "Successfull Request With Echo")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,false, TestName = "Failed Request")]
        public async Task Test_ReturnsOkResponseWithResponseContent(string model,bool isSuccess, HttpStatusCode statusCode, bool? echo)
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

        [Test]
        public async Task Test_TextCompletionStream()
        {
            var multipleQuestions =
                                    @"Q: my name is?
                                    A: timtim	
                                    ###
                                    Q: what is my date of birth
                                    A: 29 march 1980
                                    ###
                                    Q: what is the current year?
                                    A: 2022
                                    ###
                                    Q: when does timtim have his birthday and how old will he be?";

            var request = new TextCompletionRequest("text-davinci-003", multipleQuestions) { MaxTokens = 1024, N = null};

            await foreach(var t in OpenAIService.TextCompletion.GetStream(request))
            {
                Console.WriteLine(t?.Result?.Choices[0].Text);
            }
        }
    }
}
