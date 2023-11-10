namespace OpenAI.Net
{
    public static class ChatResponseFormat
    {
        public static readonly ChatResponseFormatType Text = new ChatResponseFormatType() { Type = "text" };
        public static readonly ChatResponseFormatType Json = new ChatResponseFormatType() { Type = "json_object" };
    }
}
