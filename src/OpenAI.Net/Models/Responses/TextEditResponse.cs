using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Models.Responses
{
    public class TextEditResponse
    {
        public string Object { get; set; }
        public int Created { get; set; }
        public TextEditChoice[] Choices { get; set; }
        public Usage Usage { get; set; }
    }

    public class TextEditChoice
    {
        public string Text { get; set; }
        public int Index { get; set; }
    }

}
