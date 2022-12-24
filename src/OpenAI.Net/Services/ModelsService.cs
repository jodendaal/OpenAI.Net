
using OpenAI.Net.Extensions;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;
using System.Net.Http;

namespace OpenAI.Net.Services
{
    public class ModelsService : BaseService,IModelsService
    {
        public ModelsService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<ModelsResponse, ErrorResponse>> Get()
        {
            return HttpClient.OperationGetResult<ModelsResponse, ErrorResponse>($"v1/models");
        }

        public Task<OpenAIHttpOperationResult<ModelInfo, ErrorResponse>> Get(string model)
        {
            return HttpClient.OperationGetResult<ModelInfo, ErrorResponse>($"v1/models/{model}");
        }
    }
}
