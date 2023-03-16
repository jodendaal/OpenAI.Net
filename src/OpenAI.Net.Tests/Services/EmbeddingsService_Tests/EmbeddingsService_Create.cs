﻿using System.Net;
using OpenAI.Net.Brokers;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;


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
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings");
            
            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));
            var request = new EmbeddingsRequest("The food was delicious and the waiter...", ModelTypes.TextEmbeddingAda002) { User = "test" };
            var response = await service.Create(request);
           
            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));
           
            AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "CreateList_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "CreateList_When_Fail")]
        public async Task CreateList(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings");

            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));
            var request = new EmbeddingsRequest(new List<string>() { "The food was delicious and the waiter..." }, ModelTypes.TextEmbeddingAda002) { User = "test" };
            var response = await service.Create(request);

            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "CreateWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "CreateWithExtension_When_Fail")]
        public async Task CreateWithExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings", "https://api.openai.com", (request) => { jsonRequest = request.Content.ReadAsStringAsync().Result; });

            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));;
            var response = await service.Create("The food was delicious and the waiter...", ModelTypes.TextEmbeddingAda002, "test");

            Assert.That(jsonRequest.Contains(@"""input"":[""The food was delicious and the waiter...""]"));
            Assert.That(jsonRequest.Contains(@"""user"":""test"""));


            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "CreateListWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "CreateListWithExtension_When_Fail")]
        public async Task CreateListWithExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings", "https://api.openai.com", (request) => { jsonRequest = request.Content.ReadAsStringAsync().Result; });

            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));;
            var response = await service.Create(new List<string>(){ "The food was delicious and the waiter..."}, ModelTypes.TextEmbeddingAda002, "test");

            Assert.That(jsonRequest.Contains(@"""input"":[""The food was delicious and the waiter...""]"));
            Assert.That(jsonRequest.Contains(@"""user"":""test"""));


            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "CreateWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "CreateWithExtension_When_Fail")]
        public async Task CreateWithExtensionAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings", "https://api.openai.com", (request) => { jsonRequest = request.Content.ReadAsStringAsync().Result; });

            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));;
            var response = await service.Create("The food was delicious and the waiter...",  "test");

            Assert.That(jsonRequest.Contains(@"""input"":[""The food was delicious and the waiter...""]"));
            Assert.That(jsonRequest.Contains(@"""user"":""test"""));


            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, TestName = "CreateListWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", TestName = "CreateListWithExtension_When_Fail")]
        public async Task CreateListWithExtensionAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var jsonRequest = "";
            var httpService = GetHttpService(responseStatusCode, responseJson, "/v1/embeddings", "https://api.openai.com", (request) => { jsonRequest = request.Content.ReadAsStringAsync().Result; });

            var service = new EmbeddingsService(new EmbeddingsBroker(httpService));;
            var response = await service.Create(new List<string>() { "The food was delicious and the waiter..." }, "test");

            Assert.That(jsonRequest.Contains(@"""input"":[""The food was delicious and the waiter...""]"));
            Assert.That(jsonRequest.Contains(@"""user"":""test"""));


            Assert.That(response.Result?.Data?.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data?[0]?.Embedding.Length == 3, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Usage != null, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
