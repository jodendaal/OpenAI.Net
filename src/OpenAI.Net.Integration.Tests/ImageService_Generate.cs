using OpenAI.Net.Models.Requests;
using System.Net;

namespace OpenAI.Net.Integration.Tests
{
    internal class ImageService_Generate : BaseTest
    {
        [TestCase("A cute baby sea otter", 1,true, HttpStatusCode.OK , "256x256", TestName = "Generate_When_Success")]
        [TestCase("A cute baby sea otter", 1, false, HttpStatusCode.BadRequest, "32x32", TestName = "Generate_When_Invalid_Size_Fail")]
        //[TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "Failed Request")]
        public async Task Generate(string prompt,int noOfImages,bool isSuccess, HttpStatusCode statusCode,string size)
        {
            var request = new ImageGenerationRequest(prompt) { N=noOfImages,Size= size };

            var response = await OpenAIService.Images.Genearate(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data?.Count() == noOfImages, Is.EqualTo(isSuccess), "Data is not mapped correctly");
            Assert.That(response.Result?.Data?[0].Url?.Contains("https://"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
            Assert.That(response.ErrorResponse?.Error?.Message?.Contains("is not one of ['256x256', '512x512', '1024x1024']"), isSuccess ? Is.EqualTo(null) :  Is.EqualTo(true),"Error message not returned");
        }
    }
}
