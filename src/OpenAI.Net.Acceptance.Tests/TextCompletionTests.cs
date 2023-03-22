using AutoFixture;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using FluentAssertions;
using static System.Net.Mime.MediaTypeNames;
using WireMock;

namespace OpenAI.Net.Acceptance.Tests
{
    public class TextCompletionTests : BaseTest
    {
        [Test]
        public async Task TextCompletion()
        {
            var textCompletionRequest = CreateObjectWithRandomData<TextCompletionRequest>();
            var textCompletionResponse = CreateObjectWithRandomData<TextCompletionResponse>();

            this.WireMockServer.Given(
               Request.Create()
               .WithPath("/v1/completions")
               .WithHeader("Authorization", $"Bearer {Config.Apikey}")
               .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
               .WithHeader("Content-Type", "application/json; charset=utf-8")
               .WithBody(JsonSerializer.Serialize(
                   textCompletionRequest,
                   this.JsonSerializerOptions)))
               .RespondWith(
                   Response.Create()
               .WithBody(JsonSerializer.Serialize(
                   textCompletionResponse,
                   this.JsonSerializerOptions)));


            var response  = await OpenAIService.TextCompletion.Get(textCompletionRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(textCompletionResponse);
        }

        [Test]
        public async Task TextCompletionStream()
        {
            var textCompletionRequest = CreateObjectWithRandomData<TextCompletionRequest>();
            var textCompletionResponse = CreateObjectWithRandomData<TextCompletionResponse>();
            textCompletionRequest.Stream = true;
            var responseJson = JsonSerializer.Serialize(textCompletionResponse,this.JsonSerializerOptions);
            //Remove any line feeds in json , must be jsonl (json line format)
            responseJson = responseJson.Replace("\r\n", "").Replace("\n", "");

            var responseBody = $"{responseJson}\r\n{responseJson}\r\ndata: [DONE]";

            this.WireMockServer.Given(
               Request.Create()
               .WithPath("/v1/completions")
               .WithHeader("Authorization", $"Bearer {Config.Apikey}")
               .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
               .WithHeader("Content-Type", "application/json; charset=utf-8")
               .WithBody(JsonSerializer.Serialize(
                   textCompletionRequest,
                   this.JsonSerializerOptions)))
               .RespondWith(
                  Response.Create()
               .WithBody(responseBody));


            await foreach (var response in OpenAIService.TextCompletion.GetStream(textCompletionRequest))
            {
                response.IsSuccess.Should().BeTrue();
            }
        }

    }
}