using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class ModelsTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            var responseObject = CreateObjectWithRandomData<ModelsResponse>();

            ConfigureWireMockGet("/v1/models", responseObject);

            var response = await OpenAIService.Models.Get();

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task GetById()
        {
            var responseObject = CreateObjectWithRandomData<ModelInfo>();
            var model = CreateObjectWithRandomData<string>();
            ConfigureWireMockGet($"/v1/models/{model}", responseObject);

            var response = await OpenAIService.Models.Get(model);

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().BeEquivalentTo(responseObject);
        }
    }
}
