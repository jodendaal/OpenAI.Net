namespace OpenAI.Net.Models
{
    public class OpenAIServiceRegistrationOptions
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; } = OpenAIDefaults.ApiUrl;
        public string OrganizationId { get; set; }

        public OpenAIServiceRegistrationDefaults Defaults { get; internal set; } = new OpenAIServiceRegistrationDefaults();

    }

    public class OpenAIServiceRegistrationDefaults
    {
        public string TextCompletionModel { get; set; } = OpenAIDefaults.TextCompletionModel;
        public string TextEditModel { get; set; } = OpenAIDefaults.TextEditModel;
        public string EmbeddingsModel { get; set; } = OpenAIDefaults.EmbeddingsModel;
    }


}
