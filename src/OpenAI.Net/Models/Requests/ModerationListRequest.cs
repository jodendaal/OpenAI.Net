namespace OpenAI.Net.Models.Requests
{
    public class ModerationListRequest : ModerationRequestBase<IList<string>>
    {
        public ModerationListRequest(IList<string> input)
        {
            Input = input;
        }
    }
}
