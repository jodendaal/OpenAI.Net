using OpenAI.Net.Models.Requests;
using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ImagesService_Tests
{
    internal class ImageService_Variation : BaseServiceTest
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
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "Variation_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request",TestName = "Variation_When_Fail")]
        public async Task Variation(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string jsonRequest = null;

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/variations", "https://api.openai.com", (request) =>
            {
                jsonRequest = request.Content.ReadAsStringAsync().Result;
            });

            var service = new ImageService(httpClient);
            var image = new byte[] { 1 };
            var request = new ImageVariationRequest(new Models.FileContentInfo(new byte[] { 1 }, "image.png")) { N = 2, Size = "1024x1024" };
            var response = await service.Variation(request);

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
            Assert.NotNull(jsonRequest);
            AssertResponse(response,isSuccess,errorMessage,responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "VariationWithFilePathExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "VariationWithFilePathExtension_When_Fail")]
        public async Task VariationWithFilePathExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            string formFields = "";
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/variations", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                var en = t.GetEnumerator();
                while (en.MoveNext()){
                    var f = en.Current;
                    formFields += f.Headers.ToString();
                    if(f.Headers.ToString().Contains("name=size") || f.Headers.ToString().Contains("name=n"))
                    {
                        formFields += f.ReadAsStringAsync().Result;
                    }
                    //formData = formData +=  f.ReadAsStringAsync().Result;
                }
             
            });

            var service = new ImageService(httpClient);
            var image = new byte[] { 1 };
            var response = await service.Variation(@"Images\BabyCat.png", o => {
                o.N = 2;
                o.Size = "1024x1024";
            });

            Assert.NotNull(formFields);
            Assert.That(formFields.Contains("name=size"));
            Assert.That(formFields.Contains("name=n"));
            Assert.That(formFields.Contains("1024x1024"));
            Assert.That(formFields.Contains("2"));

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));
           
            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }

        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "VariationWithBytesExtension_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "VariationWithBytesExtension_When_Fail")]
        public async Task VariationWithBytesExtension(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
           
            Dictionary<string,string> expectedFormValues = new Dictionary<string, string>();
            Dictionary<string, string> formDataErrors =  new Dictionary<string, string>();
            expectedFormValues.Add("size", "1024x1024");
            expectedFormValues.Add("n", "2");
            expectedFormValues.Add("image", @"""@file""");

            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/images/variations", "https://api.openai.com", (request) =>
            {
                var t = request.Content as MultipartFormDataContent;
                formDataErrors = ValidateFormData(t, expectedFormValues);
            });

            var service = new ImageService(httpClient);
            var image = new byte[] { 1 };

            var response = await service.Variation(image, o => {
                o.N = 2;
                o.Size = "1024x1024";
            });


            Assert.That(formDataErrors.Count, Is.EqualTo(0), $"FormData not correct {string.Join(",", formDataErrors.Select(i=> $"{i.Key}={i.Value}"))}");
            

            Assert.That(response.Result?.Data?.Count() == 2, Is.EqualTo(isSuccess));

            AssertResponse(response, isSuccess, errorMessage, responseStatusCode);
        }
    }
}
