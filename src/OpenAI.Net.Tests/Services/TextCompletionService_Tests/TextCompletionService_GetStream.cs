using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;
using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.Services.TextCompletionService_Tests
{
    internal class TextCompletionService_GetStream : BaseServiceTest
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

        [SetUp]
        public void Setup()
        {
        }


        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, false, Description = "Successfull Request", TestName = "GetStream_When_Success")]
        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, true, 2, Description = "Successfull Request Multiline", TestName = "GetStream_When_Using_Line_Data_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, Description = "Failed Request", TestName = "GetStream_When_Using_Fail")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, null, Description = "Failed Request Validation", TestName = "GetStream_When_Invalid_Model_Fail")]
        public async Task GetStream(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage, bool useMultiLineData, int expectedItemCount = 1, string modelName = "text-davinci-003")
        {
            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

            if (useMultiLineData)
            {
                var text = responseJson;
                text = $"data: {responseJson}\r\n{responseJson}\r\n[DONE]";

                responseJson = text;
            }


            var jsonRequest = "";

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);

            var request = new TextCompletionRequest(modelName, "Say this is a test");
            var itemCount = 0;
            var exceptionoccured = false;
            try
            {
                await foreach (var response in service.GetStream(request))
                {
                    AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
                    Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

                    itemCount++;
                }
            }
            catch (Exception ex)
            {
                exceptionoccured = true;
                Assert.That(isSuccess, Is.EqualTo(false));
                Assert.That(ex.Message, Is.EqualTo("The Model field is required."));
            }

            Assert.That(exceptionoccured && modelName == null || !exceptionoccured && modelName != null, Is.EqualTo(true));
            Assert.That(request.Stream, Is.EqualTo(true));
            Assert.That(itemCount, Is.EqualTo(expectedItemCount));


            if (modelName != null)
            {
                Assert.NotNull(jsonRequest);

                Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
                Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            }
        }

        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, false, Description = "Successfull Request", TestName = "GetStreamWithOptions_When_Success")]
        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, true, 2, Description = "Successfull Request Multiline", TestName = "GetStreamWithOptions_When_Using_Line_Data_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, null, Description = "Failed Request Validation", TestName = "GetStreamWithOptions_When_Invalid_Model_Fail")]
        public async Task GetStreamWithOptions(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage, bool useMultiLineData, int expectedItemCount = 1, string modelName = "text-davinci-003")
        {
            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

            if (useMultiLineData)
            {
                var text = responseJson;
                text = $"data: {responseJson}\r\n{responseJson}\r\n[DONE]";

                responseJson = text;
            }


            var jsonRequest = "";

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);

            var itemCount = 0;
            var exceptionoccured = false;
            try
            {
                await foreach (var response in service.GetStream(modelName, "Say this is a test",(o) => {
                    o.Echo = true;
                    o.LogProbs = 99;
                }))
                {
                    AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
                    Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

                    itemCount++;
                }
            }
            catch (Exception ex)
            {
                exceptionoccured = true;
                Assert.That(isSuccess, Is.EqualTo(false));
                Assert.That(ex.Message, Is.EqualTo("The Model field is required."));
            }
            Assert.That(jsonRequest?.Contains(@"""logprobs"":99"),Is.EqualTo(isSuccess));
            Assert.That(jsonRequest?.Contains(@"""echo"":true"), Is.EqualTo(isSuccess));
            Assert.That(exceptionoccured && modelName == null || !exceptionoccured && modelName != null, Is.EqualTo(true));
            Assert.That(itemCount, Is.EqualTo(expectedItemCount));


            if (modelName != null)
            {
                Assert.NotNull(jsonRequest);

                Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
                Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            }
        }

        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, false, Description = "Successfull Request", TestName = "GetStreamWithOptions_When_Success")]
        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, true, 2, Description = "Successfull Request Multiline", TestName = "GetStreamWithOptions_When_Using_Line_Data_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, null, Description = "Failed Request Validation", TestName = "GetStreamWithOptions_When_Invalid_Model_Fail")]
        public async Task GetStreamWithOptionsAndDefaultModel(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage, bool useMultiLineData, int expectedItemCount = 1, string modelName = "text-davinci-003")
        {
            OpenAIDefaults.TextCompletionModel = modelName;

            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

            if (useMultiLineData)
            {
                var text = responseJson;
                text = $"data: {responseJson}\r\n{responseJson}\r\n[DONE]";

                responseJson = text;
            }


            var jsonRequest = "";

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new TextCompletionService(httpClient);

            var itemCount = 0;
            var exceptionoccured = false;
            try
            {
                await foreach (var response in service.GetStream("Say this is a test", (o) => {
                    o.Echo = true;
                    o.LogProbs = 99;
                }))
                {
                    AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
                    Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

                    itemCount++;
                }
            }
            catch (Exception ex)
            {
                exceptionoccured = true;
                Assert.That(isSuccess, Is.EqualTo(false));
                Assert.That(ex.Message, Is.EqualTo("The Model field is required."));
            }
            Assert.That(jsonRequest?.Contains(@"""logprobs"":99"), Is.EqualTo(isSuccess));
            Assert.That(jsonRequest?.Contains(@"""echo"":true"), Is.EqualTo(isSuccess));
            Assert.That(exceptionoccured && modelName == null || !exceptionoccured && modelName != null, Is.EqualTo(true));
            Assert.That(itemCount, Is.EqualTo(expectedItemCount));


            if (modelName != null)
            {
                Assert.NotNull(jsonRequest);

                Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
                Assert.That(jsonRequest.Contains("model", StringComparison.OrdinalIgnoreCase), Is.EqualTo(true), "Serialzation options are incorrect, camel case should be used");
            }
        }
    }
}
