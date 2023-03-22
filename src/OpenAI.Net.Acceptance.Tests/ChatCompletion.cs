using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using System.Text.Json;

namespace OpenAI.Net.Acceptance.Tests
{
    internal class ChatCompletionTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(
                new ElementsBuilder<Message>(
                     Message.Create(ChatRoleType.Assistant, fixture.Create<string>()),
                     Message.Create(ChatRoleType.User, fixture.Create<string>()),
                     Message.Create(ChatRoleType.System, fixture.Create<string>())));

            var chatCompletionRequest = fixture.Create<ChatCompletionRequest>();
            var chatCompletionResponse = fixture.Create<ChatCompletionResponse>();

            ConfigureWireMockPostJson("/v1/chat/completions", chatCompletionRequest, chatCompletionResponse);

            var response = await OpenAIService.Chat.Get(chatCompletionRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(chatCompletionResponse);
        }

        [Test]
        public async Task GetStream()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(
                new ElementsBuilder<Message>(
                     Message.Create(ChatRoleType.Assistant, fixture.Create<string>()),
                     Message.Create(ChatRoleType.User, fixture.Create<string>()),
                     Message.Create(ChatRoleType.System, fixture.Create<string>())));

            var chatCompletionRequest = fixture.Create<ChatCompletionRequest>();
            chatCompletionRequest.Stream = true;

            var chatCompletionResponseList = fixture.Create<List<ChatStreamCompletionResponse>>();


            var responseBody = "data: ";
            foreach (var response in chatCompletionResponseList)
            {
                var json = JsonSerializer.Serialize(response, this.JsonSerializerOptions).Replace("\r\n", "").Replace("\n", "");
                responseBody += $"{json}\r\n";
            }
            responseBody += "[DONE]";

            ConfigureWireMockPostJson("/v1/chat/completions", chatCompletionRequest, responseBody);

            int index = 0;
            await foreach (var response in OpenAIService.Chat.GetStream(chatCompletionRequest))
            {
                response.IsSuccess.Should().BeTrue();
                response.Result.Should().BeEquivalentTo(chatCompletionResponseList[index]);
                response.Result?.Choices.Should().HaveCountGreaterThan(0);
                index++;
            }

            index.Should().Be(chatCompletionResponseList.Count);
        }
    }
}
