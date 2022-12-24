using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ModerationService_Tests
{
    internal class ModerationService_Create
    {
        const string responseJson = @"{
  ""id"": ""modr-5MWoLO"",
  ""model"": ""text-moderation-001"",
  ""results"": [
    {
      ""categories"": {
        ""hate"": false,
        ""hate/threatening"": true,
        ""self-harm"": false,
        ""sexual"": false,
        ""sexual/minors"": false,
        ""violence"": true,
        ""violence/graphic"": false
      },
      ""category_scores"": {
        ""hate"": 0.22714105248451233,
        ""hate/threatening"": 0.4132447838783264,
        ""self-harm"": 0.005232391878962517,
        ""sexual"": 0.01407341007143259,
        ""sexual/minors"": 0.0038522258400917053,
        ""violence"": 0.9223177433013916,
        ""violence/graphic"": 0.036865197122097015
      },
      ""flagged"": true
    }
  ]
}            ";

        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";


        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Create_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", Description = "Failed Request", TestName = "Create_When_Fail")]
        public async Task Create(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
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

            var service = new ModerationService(httpClient);
            var request = new ModerationRequest("input text") { Model = "test" };
            var response = await service.Create(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), $"Success was incorrect {response.ErrorMessage}");
            Assert.That(response.Result != null, Is.EqualTo(isSuccess));
            Assert.That(request.Input, Is.EqualTo("input text"));
            Assert.That(response.Result?.Results.Length > 0, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Id != null, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Model != null, Is.EqualTo(isSuccess));

            Assert.That(response.Result?.Results?[0]?.Flagged == true, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.Categories.Violence == true, Is.EqualTo(isSuccess)); ;
            Assert.That(response.Result?.Results?[0]?.Categories.Hate == false, Is.EqualTo(isSuccess)); ;
            Assert.That(response.Result?.Results?[0]?.Categories.ViolenceGraphic == false, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.Categories.SelfHarm == false, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.Categories.HateThreatening == false, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.Categories.Sexual == false, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.Categories.SexualMinors == false, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.Hate == 0.22714105248451233, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.HateThreatening == 0.4132447838783264, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.SelfHarm == 0.005232391878962517, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.Sexual == 0.01407341007143259, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.Violence == 0.9223177433013916, Is.EqualTo(isSuccess));
            Assert.That(response.Result?.Results?[0]?.CategoryScores.ViolenceGraphic == 0.036865197122097015, Is.EqualTo(isSuccess));
            Assert.That(response.StatusCode, Is.EqualTo(responseStatusCode));
            Assert.That(response.Exception == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorMessage == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Message, Is.EqualTo(errorMessage));
            Assert.That(response.ErrorResponse?.Error?.Type == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Code == null, Is.EqualTo(isSuccess));
            Assert.That(response.ErrorResponse?.Error?.Param == null, Is.EqualTo(isSuccess));
            Assert.That(path, Is.EqualTo("/v1/moderations"), "Apth is incorrect");
        }
    }
}
