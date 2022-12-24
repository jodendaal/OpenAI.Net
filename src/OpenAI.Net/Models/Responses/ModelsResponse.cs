using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Responses
{
    public class ModelsResponse
    {
        public string @Object { get; set; }
        public ModelInfo[] Data { get; set; }
    }

    public class ModelInfo
    {
        public string Id { get; set; }
        public string @Object { get; set; }
        public int Created { get; set; }

        [JsonPropertyName("owned_by")]
        public string OwnedBy { get; set; }


        public Permission[] Permission { get; set; }
        public string Root { get; set; }
        public object Parent { get; set; }
    }

    public class Permission
    {
        public string Id { get; set; }
        public string @Object { get; set; }
        public int Created { get; set; }

        [JsonPropertyName("allow_create_engine")]
        public bool AllowCreateEngine { get; set; }

        [JsonPropertyName("allow_sampling")]
        public bool AllowSampling { get; set; }

        [JsonPropertyName("allow_logprobs")]
        public bool AllowLogProbs { get; set; }

        [JsonPropertyName("allow_search_indices")]
        public bool AllowSearchIndices { get; set; }

        [JsonPropertyName("allow_view")]
        public bool AllowView { get; set; }

        [JsonPropertyName("allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }

        public string Organization { get; set; }
        public object Group { get; set; }

        [JsonPropertyName("is_blocking")]
        public bool IsBlocking { get; set; }
    }

}
