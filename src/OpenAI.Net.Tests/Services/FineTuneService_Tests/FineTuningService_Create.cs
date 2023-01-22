using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class FineTuningService_Create : BaseServiceTest
    {
        const string responseJson = @"{
    ""object"": ""fine-tune"",
    ""id"": ""ft-hqY0MqeAx8syeQfBRCjeANHN"",
    ""hyperparams"": {
      ""n_epochs"": 2,
      ""batch_size"": null,
      ""prompt_loss_weight"": 0.01,
      ""learning_rate_multiplier"": null
    },
    ""organization_id"": ""org-SOACGnGuiOQLOL0gPyGwkJzZ"",
    ""model"": ""davinci"",
    ""training_files"": [
      {
        ""object"": ""file"",
        ""id"": ""file-26a0X4VI4Ku5peEggmNyMpvt"",
        ""purpose"": ""fine-tune"",
        ""filename"": ""@TableSelect-training.jsonl"",
        ""bytes"": 3770,
        ""created_at"": 1674408518,
        ""status"": ""processed"",
        ""status_details"": null
      }
    ],
    ""validation_files"": [],
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
    ""created_at"": 1674409595,
    ""updated_at"": 1674409595,
    ""status"": ""pending"",
    ""fine_tuned_model"": null,
    ""events"": [
      {
        ""object"": ""fine-tune-event"",
        ""level"": ""info"",
        ""message"": ""Created fine-tune: ft-hqY0MqeAx8syeQfBRCjeANHN"",
        ""created_at"": 1674409595
      }
    ]
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

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "CreateWithExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "CreateWithExtension_When_Fail")]
        public async Task CreateWithExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/fine-tunes");

            var service = new FineTuneService(httpClient);
            bool actionInvoked = false;
            var response = await service.Create("myfile.jsonl", o => {
                o.ClassificationPositiveClass = "";
                o.Model = "";
                o.BatchSize = 1;
                o.ClassificationBetas = "";
                o.ClassificationNoOfClasses = 1;
                o.LearningRateMultiplier = 1;
                o.NoOfEpochs = 1;
                o.ValidationFile = "test";
                o.PromptLossWeight = 1;
                o.ComputeClassificationMetrics = 1;
                o.Suffix = "test";
                actionInvoked = true;
            });

            Assert.That(actionInvoked);
            Assert.That(response.Result?.ResultFiles.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Status == "pending", Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
