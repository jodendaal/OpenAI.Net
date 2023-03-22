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
        public async Task Get()
        {
            var textCompletionRequest = CreateObjectWithRandomData<TextCompletionRequest>();
            var textCompletionResponse = CreateObjectWithRandomData<TextCompletionResponse>();

            ConfigureWireMockPostJson("/v1/completions", textCompletionRequest, textCompletionResponse);

            var response  = await OpenAIService.TextCompletion.Get(textCompletionRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(textCompletionResponse);
        }

        [Test]
        public async Task GetStream()
        {
            var textCompletionRequest = CreateObjectWithRandomData<TextCompletionRequest>();
            textCompletionRequest.Stream = true;

            var textCompletionResponseList = CreateObjectWithRandomData<List<TextCompletionResponse>>();

            var responseBody = "data: ";
            foreach (var response in textCompletionResponseList)
            {
                var json = JsonSerializer.Serialize(response, this.JsonSerializerOptions).Replace("\r\n", "").Replace("\n", "");
                responseBody += $"{json}\r\n";
            }
            responseBody += "[DONE]";

            ConfigureWireMockPostJson("/v1/completions", textCompletionRequest, responseBody);

            int index = 0;
            await foreach (var response in OpenAIService.TextCompletion.GetStream(textCompletionRequest))
            {
                response.IsSuccess.Should().BeTrue();
                response.Result.Should().BeEquivalentTo(textCompletionResponseList[index]);
                response.Result?.Choices.Should().HaveCountGreaterThan(0);
                index++;
            }

            index.Should().Be(textCompletionResponseList.Count);
        }
    }
}