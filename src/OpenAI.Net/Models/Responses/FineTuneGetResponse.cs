namespace OpenAI.Net.Models.Responses
{
    public class FineTuneGetResponse
    {
        public string Object { get; set; }
        public FineTuneResponse[] Data { get; set; }
    }
}
