namespace OpenAI.Net.Models.Requests
{
    public class EmbeddingsListRequest : EmbeddingsRequestBase<IList<string>>
    {
        public EmbeddingsListRequest(IList<string> input, string model)
        {
            Model = model;
            Input = input;
        }
    }
}
