using OpenAI.Net.Models.Requests;

namespace OpenAI.Net.Integration.Tests
{
    public class TextCompletionService_GetStream : BaseTest
    {
        [Test]
        public async Task GetStream()
        {
            var multipleQuestions =
                                    @"Q: my name is?
                                    A: timtim	
                                    ###
                                    Q: what is my date of birth
                                    A: 29 march 1980
                                    ###
                                    Q: what is the current year?
                                    A: 2022
                                    ###
                                    Q: when does timtim have his birthday and how old will he be?";

            var request = new TextCompletionRequest("text-davinci-003", multipleQuestions) { MaxTokens = 1024, N = null};

            await foreach(var t in OpenAIService.TextCompletion.GetStream(request))
            {
                Console.WriteLine(t?.Result?.Choices[0].Text);
            }
        }
    }
}
