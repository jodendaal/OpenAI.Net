using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class StringExtensionMethods
    {

        [Test]
        public void Base64ToFileContent()
        {
            var base64 = Convert.ToBase64String(File.ReadAllBytes(@"Images\BabyCat.png"));

            var fileContent  = base64.Base64ToFileContent();

            Assert.That(fileContent, Is.Not.Null);
            Assert.That(fileContent.FileName, Is.Not.Null);
            Assert.That(fileContent.FileContent, Is.Not.Null);
        }
    }
}
