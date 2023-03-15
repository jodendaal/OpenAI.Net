using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Models;

namespace OpenAI.Net.Brokers
{
    public class TextCompletionAzureBroker : ITextCompletionBroker
    {
        private readonly IHttpService _httpService;
        private readonly AzureOpenAIConfig _azureOpenAIConfig;

        public TextCompletionAzureBroker(IHttpService httpService, AzureOpenAIConfig azureOpenAIConfig)
        {
            _httpService = httpService;
            _azureOpenAIConfig = azureOpenAIConfig;
        }

        public Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request)
        {
            return _httpService.Post<TextCompletionResponse, ErrorResponse>($"openai/deployments/{_azureOpenAIConfig.DeploymentName}/completions?api-version={_azureOpenAIConfig.ApiVersion}", request);
        }

        public IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request)
        {
            return _httpService.PostStream<TextCompletionResponse, ErrorResponse>($"openai/deployments/{_azureOpenAIConfig.DeploymentName}/completions?api-version={_azureOpenAIConfig.ApiVersion}", request);
        }
    }
}
