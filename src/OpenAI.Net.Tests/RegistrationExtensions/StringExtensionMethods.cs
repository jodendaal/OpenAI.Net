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

            Assert.NotNull(fileContent);
            Assert.NotNull(fileContent.FileName);
            Assert.NotNull(fileContent.FileContent);
        }
    }
}
