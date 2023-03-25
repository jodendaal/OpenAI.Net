namespace OpenAI.Net.Models
{
    public static class OpenAIDefaults
    {
        public static string TextCompletionModel { get; set; } = ModelTypes.TextDavinci003;
        public static string EmbeddingsModel { get; set; } = ModelTypes.TextEmbeddingAda002;
        public static string ApiUrl { get; set; } = "https://api.openai.com/";
    }
}
