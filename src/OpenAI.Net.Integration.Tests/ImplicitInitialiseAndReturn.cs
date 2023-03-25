using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using System.Net;
using System.Reflection;

namespace OpenAI.Net.Integration.Tests
{
    internal class ImplicitInitialiseAndReturn : BaseTest
    {
        [TestCase(ModelTypes.GPT35Turbo,true, HttpStatusCode.OK,TestName = "GetById_When_Success")]
        public async Task GetById(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var response = await GetResponse();

            Assert.That(response.Id == ModelTypes.GPT35Turbo, Is.EqualTo(isSuccess), "Implicit conversion failed");
        }

        private async Task<ModelInfo> GetResponse()
        {
            var result = await OpenAIService.Models.Get(ModelTypes.GPT35Turbo);
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
