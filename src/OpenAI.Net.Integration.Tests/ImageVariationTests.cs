using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class ImageVariationTests : BaseTest
    {
        [TestCase(true, HttpStatusCode.OK, "256x256")]
        [TestCase(false, HttpStatusCode.BadRequest, "32x32")]
        public async Task Test_ImageVariation(bool isSuccess,HttpStatusCode statusCode, string size)
        {
            var image = FileContentInfo.Load(@"Images\BabyOtter.png");
            var request = new ImageVariationRequest(image) { N = 1, Size = size};

            var response = await OpenAIService.Images.Variation(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data?.Count() == 1, Is.EqualTo(isSuccess), "Data is not mapped correctly");
            Assert.That(response.Result?.Data?[0].Url?.Contains("https://"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
            Assert.That(response.ErrorResponse?.Error?.Message?.Contains("is not one of ['256x256', '512x512', '1024x1024']"), isSuccess ? Is.EqualTo(null) : Is.EqualTo(true), "Error message not returned");
        }
    }
}
