using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.TextEditService_Tests
{
    internal class TextEditService_Get : BaseServiceTest
    {
        const string responseJson = @"{""object"":""edit"",""created"":1671714361,""choices"":[{""text"":""What day of the week is it?\n"",""index"":0}],""usage"":{""prompt_tokens"":25,""completion_tokens"":28,""total_tokens"":53}}";
        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Get_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request",TestName = "Get_When_Fail")]
        public async Task Get(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
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

            var service = new TextEditService(httpClient);

            var request = new TextEditRequest("text-davinci-edit-001", "Fix the spelling mistakes", "What day of the wek is it?");
            var response = await service.Get(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess));
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            Assert.That(path, Is.EqualTo("/v1/edits"));
        }
    }
}
