using Moq.Protected;
using Moq;
using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;
using OpenAI.Net.Extensions;
using System.Threading.Tasks;

namespace OpenAI.Net.Tests.Services.ImagesService_Tests
{
    internal class ImageService_Edit : BaseServiceTest
    {
        const string responseJson = @"{
              ""created"": 1589478378,
              ""data"": [
                {
                  ""url"": ""https://...""
                },
                {
                  ""url"": ""https://...""
                }
              ]
            }
            ";
        const string errorResponseJson = @"{""error"":{""message"":""an error occured"",""type"":""invalid_request_error"",""param"":""prompt"",""code"":""unsupported""}}";
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request",TestName = "Edit_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "Edit_When_Fail")]
        public async Task Edit(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);
            var image = new Models.FileContentInfo(new byte[] { 1 }, "image.png");
            var request = new ImageEditRequest("A cute baby sea otter", image) { N = 2, Size = "1024x1024", Mask = new Models.FileContentInfo(new byte[] { 1 }, "image.png") };
            var response = await service.Edit(request);

       
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            Assert.NotNull(jsonRequest);
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "EditWithExtentionFilePath_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "EditWithExtentionFilePath_When_Fail")]
        public async Task EditWithExtentionFilePath(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            Dictionary<string, string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors = new Dictionary<string, string>();
            expectedFormValues.Add("prompt", "A cute baby sea otter");
            expectedFormValues.Add("n", "99");
            expectedFormValues.Add("mask", @"""@BabyCat.png""");
            expectedFormValues.Add("image", @"""@BabyCat.png""");

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);
            });

            var service = new ImageService(httpClient);
            var response = await service.Edit("A cute baby sea otter", @"Images\BabyCat.png", o => {
                o.Mask = new Models.FileContentInfo(new byte[] { 1 }, @"BabyCat.png");
                o.N = 99;
            });


            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i => $"{i.Key}={i.Value}"))}");
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "EditWithExtentionFilePathAndMaskPath_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "EditWithExtentionFilePathAndMaskPath_When_Fail")]
        public async Task EditWithExtentionFilePathAndMaskPath(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            Dictionary<string, string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors = new Dictionary<string, string>();
            expectedFormValues.Add("prompt", "A cute baby sea otter");
            expectedFormValues.Add("n", "99");
            expectedFormValues.Add("mask", @"""@BabyCat.png""");
            expectedFormValues.Add("image", @"""@BabyCat.png""");

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);
            });

            var service = new ImageService(httpClient);
            var response = await service.Edit("A cute baby sea otter", @"Images\BabyCat.png", @"Images\BabyCat.png", o => {
                o.N = 99;
            });

            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i => $"{i.Key}={i.Value}"))}");
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "EditWithExtentionImageBytesAndMaskBytes_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "EditWithExtentionImageBytesAndMaskBytes_When_Fail")]
        public async Task EditWithExtentionImageBytesAndMaskBytes(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            Dictionary<string, string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors = new Dictionary<string, string>();
            expectedFormValues.Add("prompt", "A cute baby sea otter");
            expectedFormValues.Add("n", "99");
            expectedFormValues.Add("mask", @"""@maskImage""");
            expectedFormValues.Add("image", @"""@image""");

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);

            });

            var service = new ImageService(httpClient);
            var response = await service.Edit("A cute baby sea otter",File.ReadAllBytes(@"Images\BabyCat.png"), File.ReadAllBytes(@"Images\BabyCat.png"), o => {
                o.N = 99;
            });

            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i => $"{i.Key}={i.Value}"))}");
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "EditWithExtentionImageBytes_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, errorResponseJson, "an error occured", TestName = "EditWithExtentionImageBytes_When_Fail")]
        public async Task EditWithExtentionImageBytes(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            Dictionary<string, string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors = new Dictionary<string, string>();
            expectedFormValues.Add("prompt", "A cute baby sea otter");
            expectedFormValues.Add("n", "99");
            expectedFormValues.Add("image", @"""@file""");

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/edits", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);
            });

            var service = new ImageService(httpClient);
            var response = await service.Edit("A cute baby sea otter", File.ReadAllBytes(@"Images\BabyCat.png"), o => {
                o.N = 99;
            });

            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i => $"{i.Key}={i.Value}"))}");
            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
