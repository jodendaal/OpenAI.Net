namespace OpenAI.Net.Models.Requests
{

    public class ModerationRequest : ModerationRequestBase<string>
    {
        public ModerationRequest(string input)
        {
            Input = input;
        }
    }
}
