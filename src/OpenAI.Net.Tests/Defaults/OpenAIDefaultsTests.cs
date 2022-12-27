using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class OpenAIDefaultsTests
    {
        [Test]
        public void Test_OpenAIDefaults()
        {
            Assert.That(OpenAIDefaults.ApiUrl, Is.EqualTo("https://api.openai.com/"));
            Assert.That(OpenAIDefaults.TextCompletionModel, Is.EqualTo(ModelTypes.TextDavinci003));
            Assert.That(OpenAIDefaults.TextEditModel, Is.EqualTo(ModelTypes.TextDavinciEdit001));
            Assert.That(OpenAIDefaults.EmbeddingsModel, Is.EqualTo(ModelTypes.TextEmbeddingAda002));
        }
    }
}
