using OpenAI.Net.Models;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class OpenAIRegistrationOptionTests
    {
        [Test]
        public void Test_OpenAIRegistrationOptionDefaults()
        {
            var registrationOption = new OpenAIServiceRegistrationOptions();

            Assert.That(registrationOption.ApiUrl, Is.EqualTo(OpenAIDefaults.ApiUrl));
            Assert.That(registrationOption.OrganizationId, Is.EqualTo(null));
            Assert.That(registrationOption.ApiKey, Is.EqualTo(null));

            Assert.That(registrationOption.Defaults.TextEditModel, Is.EqualTo(OpenAIDefaults.TextEditModel));
            Assert.That(registrationOption.Defaults.TextCompletionModel, Is.EqualTo(OpenAIDefaults.TextCompletionModel));
        }
    }
}
