namespace OpenAI.Net
{
    public static class MessageExtensions
    {
        public static IList<Message> ToList(this Message value)
        {
            return new List<Message> { value };
        }
    }
}
