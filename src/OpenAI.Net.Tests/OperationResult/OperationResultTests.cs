using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Tests.TestModels;
using System.Net;

namespace OpenAI.Net.Tests.OperationResultTests
{
    public class OperationResultTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void IsSuccessIsTrueWhenResultSet()
        {
            var operation = new OperationResult<string>("a test string");

            Assert.That(operation.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public void IsSuccessIsFalseWhenResultExceptionSet()
        {
            var operation = new OperationResult<string>(new Exception("An error occurred"));

            Assert.That(operation.IsSuccess, Is.EqualTo(false));
        }

        [Test]
        public void ErrorMessageIsSet()
        {
            var message = "A custom error message";
            var operation = new OperationResult<string>(new Exception("An error occurred"), message);

            Assert.That(operation.ErrorMessage, Is.EqualTo(message));
        }

        [Test]
        public void ErrorMessageIsSetToExceptionMessageWhenNull()
        {
            var message = "A custom error message";
            var operation = new OperationResult<string>(new Exception(message));

            Assert.That(operation.ErrorMessage, Is.EqualTo(message));
        }

        [Test]
        public void ResultIsCorrectType()
        {
            var result = new TestModel() { Id = 1, Name = "Test" };
            var operation = new OperationResult<TestModel>(result);

            Assert.That(operation.Result.GetType().Name, Is.EqualTo(result.GetType().Name));
            Assert.That(operation.IsSuccess, Is.EqualTo(true));
            Assert.That(operation.Result.Id, Is.EqualTo(result.Id));
            Assert.That(operation.Result.Name, Is.EqualTo(result.Name));
        }

        [Test]
        public void ResultIsCorrectTypeInheritence()
        {
            var result = new TestModel() { Id = 1, Name = "Test" };
            var operation = new TestModelInheritence(result);

            Assert.That(operation.Result.GetType().Name, Is.EqualTo(result.GetType().Name));
            Assert.That(operation.IsSuccess, Is.EqualTo(true));
            Assert.That(operation.Result.Id, Is.EqualTo(result.Id));
            Assert.That(operation.Result.Name, Is.EqualTo(result.Name));
        }

        [Test]
        public void ExceptionISSetInheritence()
        {
            var result = new TestModel() { Id = 1, Name = "Test" };
            var operation = new TestModelInheritence(new Exception("an error occured"));

            
            Assert.That(operation.IsSuccess, Is.EqualTo(false));
            Assert.That(operation.Result, Is.EqualTo(null));
            Assert.That(operation.Exception.Message, Is.EqualTo("an error occured"));
            Assert.That(operation.ErrorMessage, Is.EqualTo("an error occured"));
        }

        [Test]
        public void ImplicitInitialiseAndReturn()
        {
            var service = new TestService();
            var result = service.ImplicitInitialiseAndReturn();

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo("Test"));
        }

        [Test]
        public void ImplicitInitialiseAndReturnHttp()
        {
            var service = new TestService();
            var result = service.ImplicitInitialiseAndReturnHttp();

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo("Test"));
        }

        [Test]
        public void ImplicitInitialiseAndReturnOpenAI()
        {
            var service = new TestService();
            var result = service.ImplicitInitialiseAndReturnHttp();

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo("Test"));
        }


        [Test]
        public void ImplicitReturnOpenAI()
        {
            var service = new TestService();
            var result = service.ImplicitReturnOpenAI();

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo("Test"));
        }
    }

    public class TestService
    {
        public TextCompletionResponse ImplicitInitialiseAndReturn()
        {
            OperationResult<TextCompletionResponse> result = new TextCompletionResponse() { Id="Test"};
            return result;
        }

        public TextCompletionResponse ImplicitInitialiseAndReturnHttp()
        {
            HttpOperationResult<TextCompletionResponse> result = new TextCompletionResponse() { Id = "Test" };
            return result;
        }

        public TextCompletionResponse ImplicitInitialiseAndReturnOpenAI()
        {
            OpenAIHttpOperationResult<TextCompletionResponse,ErrorResponse> result = new TextCompletionResponse() { Id = "Test" };
            return result;
        }

        public TextCompletionResponse ImplicitReturnOpenAI()
        {
            OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse> result = new OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>(new TextCompletionResponse() { Id = "Test" },HttpStatusCode.OK);
            return result;
        }
    }
}