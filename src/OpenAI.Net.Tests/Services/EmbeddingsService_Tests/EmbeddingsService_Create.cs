using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.EmbeddingsService_Tests
{
    internal class EmbeddingsService_Create
    {
        const string responseJson = @"{
  ""object"": ""list"",
  ""data"": [
    {
      ""object"": ""embedding"",
      ""embedding"": [
        0.0023064255,
        -0.009327292,
        -0.0028842222
      ],
      ""index"": 0
    }
  ],
  ""model"": ""text-embedding-ada-002"",
  ""usage"": {
    ""prompt_tokens"": 8,
    ""total_tokens"": 8
  }
}

            ";

        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request")]
        public async Task Test_CreateFineTune(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
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

            var service = new EmbeddingsService(httpClient);
            var request = new CreateEmbeddingsRequest("The food was delicious and the waiter...", "text-embedding-ada-002") { User = "test" };
            var response = await service.Create(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), $"Success was incorrect {response.ErrorMessage}");
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.That(path, Is.EqualTo("/v1/embeddings"), "Path is incorrect");
        }
    }
}
