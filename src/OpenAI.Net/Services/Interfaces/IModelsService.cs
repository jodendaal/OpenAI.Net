using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// List and describe the various models available in the API. <br />
    /// You can refer to the <a href="https://platform.openai.com/docs/api-reference/models"> Models</a> documentation to understand what models are available and the differences between them.
    /// </summary>
    public interface IModelsService
    {
        Task<OpenAIHttpOperationResult<ModelsResponse, ErrorResponse>> Get();
        Task<OpenAIHttpOperationResult<ModelInfo, ErrorResponse>> Get(string model);
    }
}
