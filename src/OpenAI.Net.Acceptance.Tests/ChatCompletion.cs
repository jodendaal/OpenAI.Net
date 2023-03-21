using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace OpenAI.Net.Acceptance.Tests
{
    internal class ChatCompletionTests : BaseTest
    {
        [Test]
        public async Task ChatCompletion()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(
                new ElementsBuilder<Message>(
                     Message.Create(ChatRoleType.Assistant, fixture.Create<string>()),
                     Message.Create(ChatRoleType.User, fixture.Create<string>()),
                     Message.Create(ChatRoleType.System, fixture.Create<string>())));

            var chatCompletionRequest = fixture.Create<ChatCompletionRequest>();
            var chatCompletionResponse = fixture.Create<ChatCompletionResponse>();

            this.WireMockServer.Given(
               Request.Create()
               .WithPath("/v1/chat/completions")
               .WithHeader("Authorization", $"Bearer {Config.Apikey}")
               .WithHeader("OpenAI-Organization", $"{Config.OrganizationId}")
               .WithHeader("Content-Type", "application/json; charset=utf-8")
               .WithBody(System.Text.Json.JsonSerializer.Serialize(
                   chatCompletionRequest,
                   this.JsonSerializerOptions)))
               .RespondWith(
                   Response.Create()
               .WithBody(System.Text.Json.JsonSerializer.Serialize(
                   chatCompletionResponse,
                   this.JsonSerializerOptions)));


            var response = await OpenAIService.Chat.Get(chatCompletionRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Choices.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(chatCompletionResponse);
        }
    }
}
