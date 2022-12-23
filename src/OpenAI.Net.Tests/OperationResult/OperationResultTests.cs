using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Tests.TestModels;

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
    }
}