using OpenAI.Net.Models;
using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class ImageService_Variation : BaseTest
    {
        [TestCase(true, HttpStatusCode.OK, "256x256",TestName = "Variation_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, "32x32", TestName = "Variation_When_Invalid_Size_Fail")]
        public async Task Variation(bool isSuccess,HttpStatusCode statusCode, string size)
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

        [TestCase(true, HttpStatusCode.OK, "256x256", TestName = "Variation_Base64ToFileContent_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, "32x32", TestName = "Variation_Base64ToFileContent_When_Invalid_Size_Fail")]
        public async Task Variation_Base64ToFileContent(bool isSuccess, HttpStatusCode statusCode, string size)
        {

            var generateResponse = await OpenAIService.Images.Generate("a cute baby otter", o => {
                o.N = 1;
                o.Size = "256x256";
                o.ResponseFormat = ImageResponseFormat.Base64;
                o.Style = ImageStyleOptions.Vivid;
                o.Quality = ImageQualityOptions.Standard;
                o.Model = ImageModelOptions.Dalle2;
            });


            var image = generateResponse.Result?.Data?[0].Base64.Base64ToFileContent();

            var request = new ImageVariationRequest(image) { N = 1, Size = size };

            var response = await OpenAIService.Images.Variation(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data?.Count() == 1, Is.EqualTo(isSuccess), "Data is not mapped correctly");
            Assert.That(response.Result?.Data?[0].Url?.Contains("https://"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
            Assert.That(response.ErrorResponse?.Error?.Message, isSuccess ? Is.EqualTo(null) : Contains.Substring("is not one of ['256x256', '512x512', '1024x1024'"), "Error message not returned");
        }

        [TestCase(true, HttpStatusCode.OK, "256x256", TestName = "Variation_FileInfoFileContent_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, "32x32", TestName = "Variation_FileInfoFileContent_When_Invalid_Size_Fail")]
        public async Task Variation_FileInfoFileContent(bool isSuccess, HttpStatusCode statusCode, string size)
        {

            var generateResponse = await OpenAIService.Images.Generate("a cute baby otter", o => {
                o.N = 1;
                o.Size = "256x256";
                o.ResponseFormat = ImageResponseFormat.Base64;
            });

            var image = generateResponse.Result?.Data?[0].Base64ToFileContent();

            var request = new ImageVariationRequest(image) { N = 1, Size = size };

            var response = await OpenAIService.Images.Variation(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data?.Count() == 1, Is.EqualTo(isSuccess), "Data is not mapped correctly");
            Assert.That(response.Result?.Data?[0].Url?.Contains("https://"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
            Assert.That(response.ErrorResponse?.Error?.Message, isSuccess ? Is.EqualTo(null) : Contains.Substring("is not one of ['256x256', '512x512', '1024x1024'"), "Error message not returned");
        }
    }
}
