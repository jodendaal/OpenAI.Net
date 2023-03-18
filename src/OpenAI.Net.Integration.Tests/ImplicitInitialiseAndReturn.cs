using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using System.Net;
using System.Reflection;

namespace OpenAI.Net.Integration.Tests
{
    internal class ImplicitInitialiseAndReturn : BaseTest
    {
        [TestCase("text-davinci-edit-001", true, HttpStatusCode.OK,TestName = "GetById_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,TestName = "GetById_When_Invalid_Model_Fail")]
        public async Task GetById(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var response = await GetResponse();

            Assert.That(response.Id == ModelTypes.TextDavinciEdit001, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }

        private async Task<ModelInfo> GetResponse()
        {
            var result = await OpenAIService.Models.Get(ModelTypes.TextDavinciEdit001);
            if (result.IsSuccess)
            {
                return result;
            }
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }
      
    }
}
