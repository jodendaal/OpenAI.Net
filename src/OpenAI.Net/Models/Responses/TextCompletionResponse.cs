using OpenAI.Net.Models.Responses.Common;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Responses
{
    public class TextCompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public string Model { get; set; }
        public Choice[] Choices { get; set; }
        public Usage Usage { get; set; }
        
    }

    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public object Logprobs { get; set; }
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

}
