using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IEmbeddingsService
    {
        Task<OpenAIHttpOperationResult<CreateEmbeddingsResponse, ErrorResponse>> Create(CreateEmbeddingsRequest model);
    }
}