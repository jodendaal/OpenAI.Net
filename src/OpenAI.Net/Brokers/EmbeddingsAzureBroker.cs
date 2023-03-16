using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Models;

namespace OpenAI.Net.Brokers
{
    public class EmbeddingsAzureBroker : IEmbeddingsBroker
    {

        private readonly IHttpService _httpService;
        private readonly AzureOpenAIConfig _azureOpenAIConfig;

        public EmbeddingsAzureBroker(IHttpService httpService, AzureOpenAIConfig azureOpenAIConfig)
        {
            _httpService = httpService;
            _azureOpenAIConfig = azureOpenAIConfig;
        }


        public Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model)
        {
            return _httpService.Post<EmbeddingsResponse, ErrorResponse>($"openai/deployments/{_azureOpenAIConfig.DeploymentName}/embeddings?api-version={_azureOpenAIConfig.ApiVersion}", model);
        }
    }
}
