using OpenAI.Net.Models;
using OpenAI.Net.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class ImageInfoExtensionMethodTest
    {

        [Test]
        public void ImageInfoToFileContent()
        {
            var base64 = Convert.ToBase64String(File.ReadAllBytes(@"Images\BabyCat.png"));

            var imageInfo = new ImageInfo() { Base64 = base64 };

            var fileContent  = imageInfo.Base64ToFileContent();

            Assert.That(fileContent, Is.Not.Null);
            Assert.That(fileContent.FileName, Is.Not.Null);
            Assert.That(fileContent.FileContent, Is.Not.Null);
        }
    }
}
