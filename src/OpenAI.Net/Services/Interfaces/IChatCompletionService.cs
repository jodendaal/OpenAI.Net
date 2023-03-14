using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces;

/// <summary>
/// Given a chat conversation, the model will return a chat completion response.
/// </summary>
public interface IChatCompletionService
{
    /// <summary>
    /// <inheritdoc cref="IChatCompletionService"/>
    /// </summary>
    Task<OpenAIHttpOperationResult<ChatCompletionResponse, ErrorResponse>> Get(ChatCompletionRequest request);

    /// <summary>
    /// <inheritdoc cref="IChatCompletionService"/>
    /// </summary>
    IAsyncEnumerable<OpenAIHttpOperationResult<ChatStreamCompletionResponse, ErrorResponse>> GetStream(ChatCompletionRequest request);
}