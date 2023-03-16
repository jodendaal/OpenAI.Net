namespace OpenAI.Net.Models
{
  


    public class OpenAIServiceRegistrationOptions 
    {
        public string ApiKey { get; set; }
       
        public string OrganizationId { get; set; }

        public string ApiUrl { get; set; } = OpenAIDefaults.ApiUrl;
        public OpenAIServiceRegistrationDefaults Defaults { get; internal set; } = new OpenAIServiceRegistrationDefaults();
    }

    public class OpenAIServiceAzureRegistrationOptions : AzureOpenAIConfig
    {
        public string ApiKey { get; set; }
        public string AccessToken { get; set; }
        public OpenAIServiceRegistrationDefaults Defaults { get; internal set; } = new OpenAIServiceRegistrationDefaults();
    }

 

    public class AzureOpenAIConfig
    {
        public string DeploymentName { get; set; }
        public string ApiVersion { get; set; } = "2022-12-01";
        public string ApiUrl { get; set; }
    }


    public class OpenAIServiceRegistrationDefaults
    {
        public string TextCompletionModel { get; set; } = OpenAIDefaults.TextCompletionModel;
        public string TextEditModel { get; set; } = OpenAIDefaults.TextEditModel;
        public string EmbeddingsModel { get; set; } = OpenAIDefaults.EmbeddingsModel;
    }


}
