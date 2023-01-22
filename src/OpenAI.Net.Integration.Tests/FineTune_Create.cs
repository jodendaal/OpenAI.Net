using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Net.Integration.Tests
{
    internal class FineTune_Create : BaseTest
    {

        [Test]
        public async Task Create()
        {
            var response = await OpenAIService.FineTune.Create("file-26a0X4VI4Ku5peEggmNyMpvt", o =>
            {
                o.NoOfEpochs = 2;
                o.Model = ModelTypes.Davinci;

            });

            Assert.That(response.IsSuccess, Is.EqualTo(true), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
