using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Tests.Services.EmbeddingsService_Tests
{
    internal class EmbeddingsService_Create : BaseServiceTest
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


        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "Create_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "Create_When_Fail")]
        public async Task Create(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/embeddings");

            var service = new EmbeddingsService(httpClient);
            var request = new EmbeddingsRequest("The food was delicious and the waiter...", "text-embedding-ada-002") { User = "test" };
            var response = await service.Create(request);
           
            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));
           
            AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
        }
    }
}
