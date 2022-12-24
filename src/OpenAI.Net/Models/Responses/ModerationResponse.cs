using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Responses
{
    public class ModerationResponse
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public Result[] Results { get; set; }
    }

    public class Result
    {
        public Categories Categories { get; set; }

        [JsonPropertyName("category_scores")]
        public CategoryScores CategoryScores { get; set; }
        public bool Flagged { get; set; }
    }

    public class Categories
    {
        public bool Hate { get; set; }
        [JsonPropertyName("hatethreatening")]
        public bool HateThreatening { get; set; }
        [JsonPropertyName("selfharm")]
        public bool SelfHarm { get; set; }
        public bool Sexual { get; set; }
        [JsonPropertyName("sexualminors")]
        public bool SexualMinors { get; set; }
        public bool Violence { get; set; }
        [JsonPropertyName("violencegraphic")]
        public bool ViolenceGraphic { get; set; }
    }

    public class CategoryScores
    {
        public double Hate { get; set; }
        [JsonPropertyName("hate/threatening")]
        public double HateThreatening { get; set; }
        [JsonPropertyName("self-harm")]
        public double SelfHarm { get; set; }
        public double Sexual { get; set; }
        [JsonPropertyName("sexual/minors")]
        public double SexualMinors { get; set; }
        public double Violence { get; set; }
        [JsonPropertyName("violence/graphic")]
        public double ViolenceGraphic { get; set; }
    }

}
