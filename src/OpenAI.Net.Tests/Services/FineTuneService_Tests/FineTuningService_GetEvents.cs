using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class FineTuningService_GetEvents
    {
        const string responseJson = @"{
  ""object"": ""list"",
  ""data"": [
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807352,
      ""level"": ""info"",
      ""message"": ""Job enqueued. Waiting for jobs ahead to complete. Queue number: 0.""
    },
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807356,
      ""level"": ""info"",
      ""message"": ""Job started.""
    },
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807861,
      ""level"": ""info"",
      ""message"": ""Uploaded snapshot: curie:ft-acmeco-2021-03-03-21-44-20.""
    },
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807864,
      ""level"": ""info"",
      ""message"": ""Uploaded result files: file-QQm6ZpqdNwAaVC3aSz5sWwLT.""
    },
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807864,
      ""level"": ""info"",
      ""message"": ""Job succeeded.""
    }
  ]
}
            ";

        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetEvents_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request", TestName = "Get_When_Fail")]
        public async Task GetEvents(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var res = new HttpResponseMessage { StatusCode = responseStatusCode, Content = new StringContent(responseJson) };
            var handlerMock = new Mock<HttpMessageHandler>();
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
                   path = r.RequestUri.AbsolutePath;
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };

            var service = new FineTuneService(httpClient);
            var response = await service.GetEvents("fineTuneId");

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), $"Success was incorrect {response.ErrorMessage}");
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data[0] != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data[0].Level == "info", Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.That(path, Is.EqualTo("/v1/fine-tunes/fineTuneId/events"), "Apth is incorrect");
        }
    }
}
