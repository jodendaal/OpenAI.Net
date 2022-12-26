using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class ModelsService : BaseService,IModelsService
    {
        public ModelsService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<ModelsResponse, ErrorResponse>> Get()
        {
            return HttpClient.Get<ModelsResponse, ErrorResponse>($"v1/models");
        }

        public Task<OpenAIHttpOperationResult<ModelInfo, ErrorResponse>> Get(string model)
        {
            return HttpClient.Get<ModelInfo, ErrorResponse>($"v1/models/{model}");
        }
    }
}
