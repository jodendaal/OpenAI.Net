using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.FineTuneService_Tests
{
    internal class FineTuningService_GetEvents : BaseServiceTest
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

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetEvents_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "Get_When_Fail")]
        public async Task GetEvents(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/fine-tunes/fineTuneId/events");
            var service = new FineTuneService(httpClient);
            var response = await service.GetEvents("fineTuneId");
       
            Assert.That(response.Result?.Data.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data[0] != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Object != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Data[0].Level == "info", Is.EqualTo(isSuccess));

            AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
        }
    }
}
