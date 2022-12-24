using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class CreateFineTuneTests
    {
        const string responseJson = @"{
  ""id"": ""ft-AF1WoRqd3aJAHsqc9NY7iL8F"",
  ""object"": ""fine-tune"",
  ""model"": ""curie"",
  ""created_at"": 1614807352,
  ""events"": [
    {
      ""object"": ""fine-tune-event"",
      ""created_at"": 1614807352,
      ""level"": ""info"",
      ""message"": ""Job enqueued. Waiting for jobs ahead to complete. Queue number: 0.""
    }
  ],
  ""fine_tuned_model"": null,
  ""hyperparams"": {
    ""batch_size"": 4,
    ""learning_rate_multiplier"": 0.1,
    ""n_epochs"": 4,
    ""prompt_loss_weight"": 0.1
  },
  ""organization_id"": ""org-..."",
  ""result_files"": [{
                                        ""object"": ""file"",
                                        ""id"": ""file-GB1kRstIY1YqJQBZ6rkUVphO"",
                                        ""purpose"": ""fine-tune"",
                                        ""filename"": ""@file.png"",
                                        ""bytes"": 207,
                                        ""created_at"": 1671818085,
                                        ""status"": ""processed"",
                                        ""status_details"": null
                                    }],
  ""status"": ""pending"",
  ""validation_files"": [],
  ""training_files"": [
    {
      ""id"": ""file-XGinujblHPwGLSztz8cPS8XY"",
      ""object"": ""file"",
      ""bytes"": 1547276,
      ""created_at"": 1610062281,
      ""filename"": ""my-data-train.jsonl"",
      ""purpose"": ""fine-tune-train""
    }
  ],
  ""updated_at"": 1614807352
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

            var service = new FineTuneService(httpClient);
            var request = new CreateFineTuneRequest("myfile.jsonl")
            {
                ClassificationPositiveClass = "",
                Model = "",
                BatchSize = 1,
                ClassificationBetas = "",
                ClassificationNoOfClasses = 1,
                LearningRateMultiplier = 1,
                NoOfEpochs = 1,
                ValidationFile = "test",
                PromptLossWeight = 1,
                ComputeClassificationMetrics = 1,
                Suffix = "test"
            };
            var response = await service.Create(request);

            Assert.That(request.TrainingFile, Is.EqualTo("myfile.jsonl"));
            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), $"Success was incorrect {response.ErrorMessage}");
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.ResultFiles.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Status == "pending", Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.That(path, Is.EqualTo("/v1/fine-tunes"), "Apth is incorrect");
        }
    }
}
