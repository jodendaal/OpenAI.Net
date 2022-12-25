namespace OpenAI.Net.Models.Responses
{
    public class FineTuneEventsResponse
    {
        public string Object { get; set; }
        public Event[] Data { get; set; }
    }
}
