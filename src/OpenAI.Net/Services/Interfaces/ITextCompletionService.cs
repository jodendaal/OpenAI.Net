using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    public interface ITextCompletionService
    {
        Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request);
        IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request);
    }
}