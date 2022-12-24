﻿using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Tests.OpenAIHttpClientTests
{
    internal class TextCompletionTests
    {
        const string responseJson = @"{
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
                   path = r.RequestUri.AbsolutePath;
                   jsonRequest = r.Content.ReadAsStringAsync().Result;
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };

            var openAIHttpClient = new OpenAIHttpClient(httpClient);

            var request = new TextCompletionRequest("text-davinci-003", "Say this is a test");
            var response = await openAIHttpClient.TextCompletion(request);

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
            Assert.That(path, Is.EqualTo("/v1/completions"));
        }


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request")]
        public async Task Test_TextCompletionStream(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

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
                   path = r.RequestUri.AbsolutePath;
                   jsonRequest = r.Content.ReadAsStringAsync().Result;
               });

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };

            var openAIHttpClient = new OpenAIHttpClient(httpClient);

            var request = new TextCompletionRequest("text-davinci-003", "Say this is a test");
            await foreach (var response in openAIHttpClient.TextCompletionStream(request))
            {
                Assert.That(response.IsSuccess, Is.EqualTo(isSuccess));
                Assert.That(response.Result != null, Is.EqualTo(isSuccess));
                Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));
                Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
                Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
                Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
                Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
                Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
                Assert.NotNull(jsonRequest);
                Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
                Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
              
            }

            Assert.That(path, Is.EqualTo("/v1/completions"));
        }
    }
}
