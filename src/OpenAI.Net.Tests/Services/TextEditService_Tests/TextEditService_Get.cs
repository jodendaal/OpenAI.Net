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
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/edits", "https://api.openai.com",(request) => {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextEditService(httpClient);

            var request = new TextEditRequest(ModelTypes.TextDavinciEdit001, "Fix the spelling mistakes", "What day of the wek is it?");
            var response = await service.Get(request);

         
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));
           
            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetExtensionWithOptions_When_Fail")]
        public async Task GetExtensionWithOptions(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            string jsonRequest = null;
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/edits", "https://api.openai.com", (request) => {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextEditService(httpClient);

            var response = await service.Get(ModelTypes.TextDavinciEdit001, "Fix the spelling mistakes", "What day of the wek is it?", (o =>{
                o.TopP = 0.1;
                o.Temperature = 100;
            }));

            Assert.That(jsonRequest.Contains(@"""top_p"":0.1"));
            Assert.That(jsonRequest.Contains(@"""temperature"":100"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetExtensionWithOptions_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetExtensionWithOptions_When_Fail")]
        public async Task GetExtensionWithOptionsAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            string jsonRequest = null;
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/edits", "https://api.openai.com", (request) => {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextEditService(httpClient);

            var response = await service.Get("Fix the spelling mistakes", "What day of the wek is it?", (o => {
                o.TopP = 0.1;
                o.Temperature = 100;
            }));

            Assert.That(jsonRequest.Contains(@"""top_p"":0.1"));
            Assert.That(jsonRequest.Contains(@"""temperature"":100"));

            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

            Assert.NotNull(jsonRequest);
            Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
            Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
