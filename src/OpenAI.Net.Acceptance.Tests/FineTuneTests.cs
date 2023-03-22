using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Acceptance.Tests
{
    public class FineTuneTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            var responseObject = CreateObjectWithRandomData<FineTuneGetResponse>();

            ConfigureWireMockGet("/v1/fine-tunes", responseObject);

            var response = await OpenAIService.FineTune.Get();

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task GetById()
        {
            var responseObject = CreateObjectWithRandomData<FineTuneResponse>();
            var fineTuneId = "1";
            ConfigureWireMockGet($"/v1/fine-tunes/{fineTuneId}", responseObject);

            var response = await OpenAIService.FineTune.Get(fineTuneId);

            response.IsSuccess.Should().BeTrue();
            response.Result?.ResultFiles.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task Cancel()
        {
            var responseObject = CreateObjectWithRandomData<FineTuneResponse>();
            var fineTuneId = "1";
            ConfigureWireMockGet($"/v1/fine-tunes/{fineTuneId}/cancel", responseObject);

            var response = await OpenAIService.FineTune.Cancel(fineTuneId);

            response.IsSuccess.Should().BeTrue();
            response.Result?.ResultFiles.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task GetEvents()
        {
            var responseObject = CreateObjectWithRandomData<FineTuneEventsResponse>();
            var fineTuneId = "1";
            ConfigureWireMockGet($"/v1/fine-tunes/{fineTuneId}/events", responseObject);

            var response = await OpenAIService.FineTune.GetEvents(fineTuneId);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task Create()
        {
            var fineTuneRequest = CreateObjectWithRandomData<FineTuneRequest>();
            var responseObject = CreateObjectWithRandomData<FineTuneResponse>();
            
            ConfigureWireMockPostJson($"/v1/fine-tunes", fineTuneRequest,responseObject);

            var response = await OpenAIService.FineTune.Create(fineTuneRequest);

            response.IsSuccess.Should().BeTrue();
            response.Result?.ResultFiles.Should().HaveCountGreaterThan(1);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task Delete()
        {
            var responseObject = CreateObjectWithRandomData<DeleteResponse>();

            var model = "1";
            ConfigureWireMockDelete($"/v1/models/{model}", responseObject);

            var response = await OpenAIService.FineTune.Delete(model);

            response.IsSuccess.Should().BeTrue();
            response.Result.Should().BeEquivalentTo(responseObject);
        }
    }
}
