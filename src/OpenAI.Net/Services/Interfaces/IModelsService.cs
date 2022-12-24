using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IModelsService
    {
        Task<OpenAIHttpOperationResult<ModelsResponse, ErrorResponse>> Get();
        Task<OpenAIHttpOperationResult<ModelInfo, ErrorResponse>> Get(string model);
    }
}
