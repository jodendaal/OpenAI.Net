using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;

namespace OpenAI.Net.Brokers
{
    public interface ITextCompletionBroker
    {
        Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request);

        IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request);
    }
}
