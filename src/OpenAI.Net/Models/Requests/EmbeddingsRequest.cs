namespace OpenAI.Net.Models.Requests
{
    public class EmbeddingsRequest : EmbeddingsRequestBase<string>
    {
        public EmbeddingsRequest(string input, string model)
        {
            Model = model;
            Input = input;
        }
    }
}
