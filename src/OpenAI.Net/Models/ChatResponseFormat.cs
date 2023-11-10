namespace OpenAI.Net
{
    public static class ChatResponseFormat
    {
        public static readonly object Text = new { type = "text" };
        public static readonly object Json = new { type = "json_object" };
    }
}
