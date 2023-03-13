using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Tests.RegistrationExtensions
{
    public class MessageExtensionMethods
    {

        [Test]
        public void SinggeMessageToList()
        {
            var message = new Message(ChatRoleType.User, "Test");

            var list = message.ToList();

            Assert.That(list.Count , Is.EqualTo(1));
            Assert.That(list.FirstOrDefault().Role, Is.EqualTo(ChatRoleType.User));
            Assert.That(list.FirstOrDefault().Content, Is.EqualTo("Test"));
        }
    }
}
