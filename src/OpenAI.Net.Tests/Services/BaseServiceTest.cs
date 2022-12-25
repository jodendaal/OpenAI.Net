using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Tests.Services
{
    public class BaseServiceTest
    {

        public const string ErrorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";

        public HttpClient GetHttpClient(HttpStatusCode responseStatusCode, HttpResponseMessage httpResponseMessage, string path, string url = "https://api.openai.com", Action<HttpRequestMessage> onRequest = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => httpResponseMessage)
               .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
               {
                   if (onRequest != null)
                   {
                       onRequest(r);
                   }

                   Assert.That(r.RequestUri.AbsolutePath, Is.EqualTo(path), "Path is incorrect");
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(url) };

            return httpClient;
        }

        public HttpClient GetHttpClient(HttpStatusCode responseStatusCode,string responseJson,string path,string url = "https://api.openai.com", Action<HttpRequestMessage> onRequest = null)
        {
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res)
               .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
               {
                   if(onRequest!= null)
                   {
                       onRequest(r);
                   }
                  
                   Assert.That(r.RequestUri.AbsolutePath, Is.EqualTo(path), "Path is incorrect");
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(url) };

            return httpClient;
        }

        public void AssertResponse<T>(OpenAIHttpOperationResult<T,ErrorResponse> response,bool isSuccess,string errorMessage,HttpStatusCode responseStatusCode)
        {
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), $"Success was incorrect {response.ErrorMessage}");
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
        }
       

    }
}
