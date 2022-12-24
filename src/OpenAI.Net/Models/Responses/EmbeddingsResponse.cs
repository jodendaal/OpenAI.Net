using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Models.Responses
{
    public class EmbeddingsResponse 
    {
        public EmbeddingInfo[] Data { get; set; }
        public string @Object { get; set; }

        public string Model { get; set; }
        public Usage Usage { get; set; }
    }

    public class EmbeddingInfo
    {
        public int? Index { get; set; }

        public double[] Embedding { get; set; }
    }
}
