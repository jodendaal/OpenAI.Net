namespace OpenAI.Net.Models
{
    public static class OpenAIDefaults
    {
        public static string TextCompletionModel { get; set; } = "text-davinci-003";
        public static string TextEditModel { get; set; } = "text-davinci-edit-001";
        public static string ApiUrl { get; set; } = "https://api.openai.com/";
    }
}
