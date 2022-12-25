using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class FineTuningService_Create : BaseServiceTest
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


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "Create_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request",TestName = "Create_When_Fail")]
        public async Task Create(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/fine-tunes");

            var service = new FineTuneService(httpClient);
            var request = new FineTuneRequest("myfile.jsonl")
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
            Assert.That(response.Result?.ResultFiles.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Status == "pending", Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
