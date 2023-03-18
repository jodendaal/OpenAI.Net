namespace OpenAI.Net.Models
{
    public static class OpenAIDefaults
    {
        public static ModelTypes TextCompletionModel { get; set; } = ModelTypes.TextDavinci003;
        public static ModelTypes TextEditModel { get; set; } = ModelTypes.TextDavinciEdit001;
        public static ModelTypes EmbeddingsModel { get; set; } = ModelTypes.TextEmbeddingAda002;
        public static string ApiUrl { get; set; } = "https://api.openai.com/";
    }
}
