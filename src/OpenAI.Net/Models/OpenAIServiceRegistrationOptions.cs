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
        public ModelTypes TextCompletionModel { get; set; } = OpenAIDefaults.TextCompletionModel;
        public ModelTypes TextEditModel { get; set; } = OpenAIDefaults.TextEditModel;
        public ModelTypes EmbeddingsModel { get; set; } = OpenAIDefaults.EmbeddingsModel;
    }


}
