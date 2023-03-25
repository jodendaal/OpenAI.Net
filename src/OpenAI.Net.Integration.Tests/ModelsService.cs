using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Reflection;

namespace OpenAI.Net.Integration.Tests
{
    internal class ModelsService : BaseTest
    {
        [TestCase(ModelTypes.GPT35Turbo,true, HttpStatusCode.OK,TestName = "GetById_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound,TestName = "GetById_When_Invalid_Model_Fail")]
        public async Task GetById(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var response = await OpenAIService.Models.Get(model);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Id == ModelTypes.GPT35Turbo, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }

        [TestCase(true, HttpStatusCode.OK)]
        public async Task GetAll(bool isSuccess, HttpStatusCode statusCode)
        {
            var response = await OpenAIService.Models.Get();
            
            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Data.Count() > 0, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
        }

        [TestCase(true, HttpStatusCode.OK)]
        public async Task ValidateModelTypesModel(bool isSuccess, HttpStatusCode statusCode)
        {
            var response = await OpenAIService.Models.Get();
            var allModels = response?.Result?.Data.Select(i => i.Id).ToList()!;
            var definedModelTypes = typeof(ModelTypes).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Select(fieldInfo => fieldInfo.GetRawConstantValue()?.ToString());

            var invalidModelTypes = definedModelTypes.Where(model => !allModels.Contains(model)).ToList();
            var missingModelTypes = allModels.Where(id => !id.Contains("ft-personal") && !id.Contains("gpt-3.5-turbo-0301")).Where(model => !definedModelTypes.Contains(model)).ToList();


            Assert.That(invalidModelTypes.Count, Is.EqualTo(0), $"Invalid models found {string.Join("\r\n", invalidModelTypes)}");
            Assert.That(missingModelTypes.Count, Is.EqualTo(0), $"Missing models found {string.Join("\r\n", missingModelTypes)}");
        }
    }
}
