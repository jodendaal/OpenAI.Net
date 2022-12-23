namespace OpenAI.Net.Models.Responses
{
    public class DeleteFileResponse
    {
        public string Id { get; set; }
        public string @Object { get; set; }
        public bool Deleted { get; set; }
    }
}
