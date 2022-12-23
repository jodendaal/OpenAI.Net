using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Responses
{
    public class FileListResponse
    {
        public FileInfoResponse[] Data { get; set; }
        public string @Object { get; set; }
    }

    public class FileInfoResponse
    {
        public string Id { get; set; }
        public string @Object { get; set; }
        public int Bytes { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
        public string Filename { get; set; }
        public string Purpose { get; set; }
    }

}
