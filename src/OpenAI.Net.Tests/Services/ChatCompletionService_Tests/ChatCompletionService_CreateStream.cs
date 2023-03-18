﻿using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;
using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.Services.ChatCompletionService_Tests
{
    internal class ChatCompletionService_CreateStream : BaseServiceTest
    {
        const string responseJson = @"{""id"":""chatcmpl-6tjHFgt6qt4P53kSHH9Oym901x2CT"",
                                    ""object"":""chat.completion.chunk"",""created"":1678740105,""model"":""gpt-3.5-turbo-0301"",
                                    ""choices"":[{""delta"":{""content"":""\n\n""},""index"":0,""finish_reason"":null}]}
                                    ";

        [SetUp]
        public void Setup()
        {
        }


        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, false, Description = "Successfull Request", TestName = "CreateStream_When_Success")]
        [TestCase(true, HttpStatusCode.OK, $"{responseJson}", null, true, 2, Description = "Successfull Request Multiline", TestName = "CreateStream_When_Using_Line_Data_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, Description = "Failed Request", TestName = "CreateStream_When_Using_Fail")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", false, 0, "invalid-model", Description = "Failed Request Validation", TestName = "CreateStream_When_Invalid_Model_Fail")]
        public async Task CreateStream(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage, bool useMultiLineData, int expectedItemCount = 1, string modelName = "gpt-3.5-turbo")
        {
            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

            if (useMultiLineData)
            {
                var text = responseJson;
                text = $"data: {responseJson}\r\n{responseJson}\r\n[DONE]";

                responseJson = text;
            }


            var jsonRequest = "";

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/chat/completions", "https://api.openai.com", (request) => {
                jsonRequest = jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ChatCompletionService(httpClient);

            var message = new Message(ChatRoleType.User, "Say this is a test");
            var request = new ChatCompletionRequest(message,modelName);
            var itemCount = 0;
            //var exceptionoccured = false;
            //try
            //{
                await foreach (var response in service.CreateStream(request))
                {
                    AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
                    Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess));

                    itemCount++;
                }
            //}
            //catch (Exception ex)
            //{
            //    exceptionoccured = true;
            //    Assert.That(isSuccess, Is.EqualTo(false));
            //    Assert.That(ex.Message, Is.EqualTo("The Model field is required."));
            //}

            //Assert.That(exceptionoccured && modelName == null || !exceptionoccured && modelName != null, Is.EqualTo(true));
            Assert.That(request.Stream, Is.EqualTo(true));
            Assert.That(itemCount, Is.EqualTo(expectedItemCount));


                Assert.NotNull(jsonRequest);

                Assert.That(jsonRequest.Contains("best_of"), Is.EqualTo(false), "Serialzation options are incorrect, null values should not be serialised");
        }
    }
}
