using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Given a prompt, the model will return one or more predicted completions, <br />
    /// and can also return the probabilities of alternative tokens at each position.
    /// </summary>
    public interface ITextCompletionService
    {
        /// <summary>
        /// <inheritdoc cref="ITextCompletionService"/>
        /// </summary>
        Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request);

        /// <summary>
        /// <inheritdoc cref="ITextCompletionService"/>
        /// </summary>
        IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request);
    }
}