using OpenAI.Net.Models.Requests;
using System.Net;
using System.Text.Json;

namespace OpenAI.Net.Integration.Tests
{
    internal class TextEditService : BaseTest
    {
        [TestCase("text-davinci-003", true, HttpStatusCode.OK ,TestName = "Get_When_Success")]
        [TestCase("invalid_model", false, HttpStatusCode.NotFound, TestName = "Get_When_Fail")]
        public async Task Get(string model,bool isSuccess, HttpStatusCode statusCode)
        {
            var request = new TextEditRequest(model, "Fix the spelling mistakes", "What day of the wek is it?");

            var response = await OpenAIService.TextEdit.Get(request);

            Assert.That(response.IsSuccess, Is.EqualTo(isSuccess), "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(statusCode));
            Assert.That(response.Result?.Choices?.Count() == 1, Is.EqualTo(isSuccess), "Choices are not mapped correctly");
            Assert.That(response.Result?.Choices?[0].Text?.Contains("What day of the week is it"), isSuccess ? Is.EqualTo(isSuccess) : Is.EqualTo(null), "Choice text not set");
        }

        [Test]
        public async Task SaveAsJson()
        {
            try
            {
                var textContent = File.ReadAllText("C:\\Development\\github\\OpenAI.NET\\README.md");

                var test = new { text = textContent };
                var json = JsonSerializer.Serialize(test);
                File.WriteAllText("C:\\Development\\github\\OpenAI.NET\\README.json", json);

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
                httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Awesome-Octocat-App");

                var response  = await httpClient.PostAsync("https://api.github.com/markdown", new StringContent(json,System.Text.Encoding.UTF8 , "text/plain"));
                var html = await response.Content.ReadAsStringAsync();

                html = $"<html><head><meta name=\"google-site-verification\" content=\"X0R8Gul3ewm3LbDbMchm5TztVHXrCvPKyd60P5CdEC0\" /> <meta charset=\"UTF-8\"></head><body>{html}</body></html>";
                html = html.Replace(@"href=""#openainet""", @"href=""https://github.com/jodendaal/OpenAI.Net""");
                File.WriteAllText("C:\\Development\\github\\OpenAI.NET\\docs\\index.html", html);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
