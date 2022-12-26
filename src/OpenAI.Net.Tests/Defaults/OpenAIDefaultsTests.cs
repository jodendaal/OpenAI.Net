using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class OpenAIDefaultsTests
    {
        [Test]
        public void Test_OpenAIDefaults()
        {
            Assert.That(OpenAIDefaults.ApiUrl, Is.EqualTo("https://api.openai.com/"));
            Assert.That(OpenAIDefaults.TextCompletionModel, Is.EqualTo("text-davinci-003"));
            Assert.That(OpenAIDefaults.TextEditModel, Is.EqualTo("text-davinci-edit-001"));
            Assert.That(OpenAIDefaults.EmbeddingsModel, Is.EqualTo("text-embedding-ada-002"));
        }
    }
}
