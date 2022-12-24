using Moq.Protected;
using Moq;
using System.Net;
using OpenAI.Net.Extensions;
using OpenAI.Net.Tests.TestModels;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.HttpClientTests
{
    internal class HttpClientExtensionTest
    {
        //QQQ Change this to test model 
        const string jsonResponseString = @"{
                ""id"": ""1"",
                ""name"": ""test""
               
            }";

        const string jsonResponseErrorString = @"{""error"":{""message"":""an error occured""}}";
        const string invalidUriMessage = "An invalid request URI was provided. Either the request URI must be an absolute URI or BaseAddress must be set.";

        [SetUp]
        public void Setup()
        {

        }

        [TestCase(HttpStatusCode.OK, true, false, "image.png", "https://api.openai.com/v1/completions", null, null, jsonResponseString, false, Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, "image.png", "https://api.openai.com/v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString, true, Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, "image.png", "", invalidUriMessage, invalidUriMessage, jsonResponseString, false, Description = "Url error")]
        [TestCase(HttpStatusCode.OK, true, false, null, "https://api.openai.com/v1/completions", null, null, jsonResponseString, false, Description = "Null Filename is defaulted")]
        public async Task Test_OperationGetFileResult(HttpStatusCode httpStatusCode, bool isSuccess, bool resultIsNull, string fileName, string url, string exceptionMessage, string errorMessage, string jsonResult, bool errorResponseIsSet)
        {
            var imageEditRequest = new ImageEditRequest("a baby fish", new Models.FileContentInfo(new byte[] { 1 }, fileName));
            var formDataContent = imageEditRequest.ToMultipartFormDataContent();
            formDataContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            if (fileName != null)
            {
                formDataContent.Headers.ContentDisposition.FileName = $@"""{fileName}""";
            }

            var jsonContent = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = isSuccess ? formDataContent : jsonContent };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object);
            var response = await httpClient.OperationGetFileResult<ErrorResponse>(url);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.FileName == (fileName == null ? "file" : fileName), Is.EqualTo(isSuccess), "Filename incorrect");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }


        [TestCase(HttpStatusCode.OK, true, false, 1, "https://api.openai.com/v1/completions", null, null, jsonResponseString, false, Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "https://api.openai.com/v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString, true, Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "", invalidUriMessage, invalidUriMessage, jsonResponseString, false, Description = "Url error")]
        public async Task Test_OperationDeleteResult(HttpStatusCode httpStatusCode, bool isSuccess, bool resultIsNull, int? responseId, string url, string exceptionMessage, string errorMessage, string jsonResult, bool errorResponseIsSet)
        {
            HttpContent content = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = content };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object);
            var response = await httpClient.OperationDeleteResult<TestModelResponse, ErrorResponse>(url);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.Id, Is.EqualTo(responseId), "Response not deserialized correctly");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }

        [TestCase(HttpStatusCode.OK, true, false, 1, "https://api.openai.com/v1/completions", null, null, jsonResponseString, false,  Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "https://api.openai.com/v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString, true,  Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "", invalidUriMessage, invalidUriMessage, jsonResponseString, false, Description = "Url error")]
        public async Task Test_OperationGetResult(HttpStatusCode httpStatusCode, bool isSuccess, bool resultIsNull, int? responseId, string url, string exceptionMessage, string errorMessage, string jsonResult, bool errorResponseIsSet)
        {
            HttpContent content = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = content };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object);
            var response = await httpClient.OperationGetResult<TestModelResponse, ErrorResponse>(url);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.Id, Is.EqualTo(responseId), "Response not deserialized correctly");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }



        [TestCase(HttpStatusCode.OK,true,false, 1, "v1/completions",null,null,jsonResponseString,false,1, Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString,true,1, Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "v1/completions", "The Id field is required.", "The Id field is required.", jsonResponseString, false,null,  Description = "Model validation Request")]
        public async Task Test_OperationPostResult(HttpStatusCode httpStatusCode,bool isSuccess,bool resultIsNull,int? responseId,string url,string exceptionMessage,string errorMessage,string jsonResult,bool errorResponseIsSet,int? requestId)
        {
            HttpContent content = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = content };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.openai.com") };
            var model = new TestModel() { Id = requestId };
            var response = await httpClient.OperationPostResult<TestModelResponse, ErrorResponse>(url, model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.Id, Is.EqualTo(responseId), "Response not deserialized correctly");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }

        [TestCase(HttpStatusCode.OK, true, false, 1, "https://api.openai.com/v1/completions", null, null, jsonResponseString, false, 1, Description = "Sucessfull Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "https://api.openai.com/v1/completions", "BadRequest", jsonResponseErrorString, jsonResponseErrorString, true, 1, Description = "Failed Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "https://api.openai.com/v1/completions", "The Id field is required.", "The Id field is required.", jsonResponseString, false, null, Description = "Model validation Request")]
        [TestCase(HttpStatusCode.BadRequest, false, true, null, "", invalidUriMessage, invalidUriMessage, jsonResponseString, false, 1, TestName = "Invalid Url")]
        public async Task Test_OperationPostFormResult(HttpStatusCode httpStatusCode, bool isSuccess, bool resultIsNull, int? responseId, string url, string exceptionMessage, string errorMessage, string jsonResult, bool errorResponseIsSet, int? requestId)
        {
            HttpContent content = new StringContent(jsonResult);

            var res = new HttpResponseMessage { StatusCode = httpStatusCode, Content = content };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => res);

            var httpClient = new HttpClient(handlerMock.Object);
            
            var model = new TestModel() { Id = requestId };
            var response = await httpClient.OperationPostFormResult<TestModelResponse, ErrorResponse>(url, model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "IsSuccess not set correctly for successfull operation");
            Assert.That(response.Result == null, Is.EqualTo(resultIsNull), "Result object was null");
            Assert.That(response.Result?.Id, Is.EqualTo(responseId), "Response not deserialized correctly");
            Assert.That(response.Exception?.Message, Is.EqualTo(exceptionMessage), "Exception not set");
            Assert.That(response.ErrorMessage, Is.EqualTo(errorMessage), "ErrorMessage not set");
            Assert.That(response.ErrorResponse != null, Is.EqualTo(errorResponseIsSet), "ErrorResponse not set");
            Assert.That(response.StatusCode, Is.EqualTo(httpStatusCode), "StatusCode not set");
        }

        [Test]
        public async Task Test_ToMultipartFormDataContent()
        {
            var imageEditRequest = new ImageEditRequest("a baby fish", new Models.FileContentInfo(new byte[] { 1 },"image.png")) { Mask = new Models.FileContentInfo(new byte[] { 2 }, "image2.png") };
            var formDataContent = imageEditRequest.ToMultipartFormDataContent();

            var bytes = await formDataContent.ReadAsByteArrayAsync();
            var stringData = await formDataContent.ReadAsStringAsync();
            var fileName = formDataContent.Headers?.ContentDisposition?.FileName?.Replace(@"""", "");

            Assert.NotNull(stringData);
            Assert.NotNull(bytes.Length > 1);
            Assert.NotNull(formDataContent);
            Assert.That(stringData.Contains(@"Content-Disposition: form-data; name=image; filename=""@image.png"";"),Is.EqualTo(true));
            Assert.That(stringData.Contains(@"Content-Disposition: form-data; name=mask; filename=""@image2.png"";"), Is.EqualTo(true));
            Assert.That(stringData.Contains(@"Content-Disposition: form-data; name=prompt"), Is.EqualTo(true));
        }

        [Test]
        public async Task Test_ToHttpContent()
        {
            var bytesValue = new byte[] { 1 };
            var stringValue = "a test";

            var bytesContent = bytesValue.ToHttpContent();
            var bytesRead = await bytesContent.ReadAsByteArrayAsync();

            var stringContent = stringValue.ToHttpContent();
            var stringRead = await stringContent.ReadAsStringAsync();

            var testNull  = HttpClientExtensions.ToHttpContent(null);
            var testNullRead = await testNull.ReadAsStringAsync();

            Assert.NotNull(bytesContent);
            Assert.That(bytesContent.GetType(), Is.EqualTo(typeof(ByteArrayContent)));
            Assert.That(bytesRead, Is.EqualTo(bytesValue));

            Assert.NotNull(stringContent);
            Assert.That(stringContent.GetType(), Is.EqualTo(typeof(StringContent)));
            Assert.That(stringRead, Is.EqualTo(stringValue));

            Assert.NotNull(testNull);
            Assert.That(testNull.GetType(), Is.EqualTo(typeof(StringContent)));
            Assert.That(testNullRead, Is.EqualTo(""));
        }

        [Test]
        public void Test_GetPropertyName()
        {
            var imageEditRequest = new ImageEditRequest("a baby fish", new Models.FileContentInfo(new byte[] { 1 }, "image.png")) { ResponseFormat = "url" };
            
            var propWithJsonNameAttribute = imageEditRequest.GetType().GetProperties().FirstOrDefault(i => i.Name == "ResponseFormat");
            var propertyNameWithJsonNameAttribute = propWithJsonNameAttribute.GetPropertyName();
            Assert.That(propertyNameWithJsonNameAttribute, Is.EqualTo("response_format"));

            var propWithNoAttribute = imageEditRequest.GetType().GetProperties().FirstOrDefault(i => i.Name == "Prompt");
            var propertyNameWithNoAttribute = propWithNoAttribute.GetPropertyName();
            Assert.That(propertyNameWithNoAttribute, Is.EqualTo("prompt"));
        }

        [Test]
        public void Test_LoadAndSaveFile()
        {
            var file = FileContentInfo.Load(@"Images\BabyCat.png");

            Assert.That(file.FileName, Is.EqualTo("BabyCat.png"));
            Assert.That(file.FileContent.Length > 0, Is.EqualTo(true));

            var saveFileName = $"BabyCatSaved{Guid.NewGuid()}.png";
            file.Save(saveFileName);

            Assert.That(File.Exists(saveFileName),Is.EqualTo(true));
        }
    }
}
