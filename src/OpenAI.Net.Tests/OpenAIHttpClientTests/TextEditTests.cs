using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Tests.OpenAIHttpClientTests
{
    internal class TextEditTests
    {
        const string responseJson = @"{""object"":""edit"",""created"":1671714361,""choices"":[{""text"":""What day of the week is it?\n"",""index"":0}],""usage"":{""prompt_tokens"":25,""completion_tokens"":28,""total_tokens"":53}}";
        const string errorResponseJson = @"{""error"":{""message"":""an error occured""}}";
        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase(true, HttpStatusCode.OK, responseJson,null, Description = "Successfull Request")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request")]
        public async Task Test_TextCompletion(bool isSuccess,HttpStatusCode responseStatusCode,string responseJson,string errorMessage)
        {
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            string jsonRequest = null;
            string path = null;
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res)
               .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
               {
                   jsonRequest = r.Content.ReadAsStringAsync().Result;
                   path = r.RequestUri.AbsolutePath;
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };

            var openAIHttpClient = new OpenAIHttpClient(httpClient);

            var request = new TextEditRequest("text-davinci-edit-001", "Fix the spelling mistakes", "What day of the wek is it?");
            var response = await openAIHttpClient.TextEdit(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess));
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false),"Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model",StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            Assert.That(path, Is.EqualTo("/v1/edits"));
        }
    }
}
