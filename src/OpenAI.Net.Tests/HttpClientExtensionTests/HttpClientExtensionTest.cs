using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Extensions;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Tests.TestModels;

namespace OpenAI.Net.Tests.HttpClientTests
{
    internal class HttpClientExtensionTest
    {
        //QQQ Change this to test model 
        const string jsonResponseString = @"{
                ""id"": ""cmpl-6PtAJQgmP51aSZDoG1PFuorDwP9aZ"",
                ""object"": ""text_completion"",
                ""created"": 1671628275,
                ""model"": ""text-davinci-003"",
                ""choices"": [
                    {
                        ""text"": ""\n\nThis is indeed a test"",
                        ""index"": 0,
                        ""logprobs"": null,
                        ""finish_reason"": ""length""
                    }
                ],
                ""usage"": {
                    ""prompt_tokens"": 5,
                    ""completion_tokens"": 7,
                    ""total_tokens"": 12
                }
            }";

        const string jsonResponseErrorString = @"{""error"":{""message"":""an error occured""}}";


        [SetUp]
        public void Setup()
        {

        }

        [TestCase(HttpStatusCode.OK,true,false, "cmpl-6PtAJQgmP51aSZDoG1PFuorDwP9aZ", "v1/completions",null,null,jsonResponseString,false,1, Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString,true,1, Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "v1/completions", "The Id field is required.", "The Id field is required.", jsonResponseString, false, null, Description = "Model validation Request")]
        public async Task Test_OperationPostResult(HttpStatusCode httpStatusCode,bool isSuccess,bool resultIsNull,string id,string url,string exceptionMessage,string errorMessage,string jsonResult,bool errorResponseIsSet,int? modelId)
        {
            HttpContent content = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = content };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };
            var model = new TestModel() { Id = modelId };
            var response = await httpClient.OperationPostResult<TextCompletionResponse, ErrorResponse>(url, model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.Id, Is.EqualTo(id), "Response not deserialized correctly");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }
    }
}
