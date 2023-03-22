using FluentAssertions;
using NUnit.Framework;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Acceptance.Tests
{
    public class ImageTests : BaseTest
    {
        [Test]
        public async Task Generate()
        {
            var request = CreateObjectWithRandomData<ImageGenerationRequest>();
            var responseObject = CreateObjectWithRandomData<ImageGenerationResponse>();

            ConfigureWireMockPostJson("/v1/images/generations", request, responseObject);

            var response = await OpenAIService.Images.Generate(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Length.Should().BeGreaterThan(0);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task Edit()
        {
            var request = CreateObjectWithRandomData<ImageEditRequest>();
            var responseObject = CreateObjectWithRandomData<ImageGenerationResponse>();

            ConfigureWireMockPostForm("/v1/images/edits", request, responseObject);

            var response = await OpenAIService.Images.Edit(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Length.Should().BeGreaterThan(0);
            response.Result.Should().BeEquivalentTo(responseObject);
        }

        [Test]
        public async Task Variation()
        {
            var request = CreateObjectWithRandomData<ImageVariationRequest>();
            var responseObject = CreateObjectWithRandomData<ImageGenerationResponse>();

            ConfigureWireMockPostForm("/v1/images/variations", request, responseObject);

            var response = await OpenAIService.Images.Variation(request);

            response.IsSuccess.Should().BeTrue();
            response.Result?.Data.Length.Should().BeGreaterThan(0);
            response.Result.Should().BeEquivalentTo(responseObject);
        }
    }
}
