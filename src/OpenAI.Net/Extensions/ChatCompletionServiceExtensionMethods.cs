using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Models;

namespace OpenAI.Net
{
    public static class ChatCompletionServiceExtensionMethods
    {
        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static Task<OpenAIHttpOperationResult<ChatCompletionResponse, ErrorResponse>> Get(this IChatCompletionService chatCompletion, string userMessage, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(Message.Create(ChatRoleType.User, userMessage));
            options?.Invoke(request);
            return chatCompletion.Create(request);
        }

        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static Task<OpenAIHttpOperationResult<ChatCompletionResponse, ErrorResponse>> Get(this IChatCompletionService chatCompletion, IList<Message> messages, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(messages);
            options?.Invoke(request);
            return chatCompletion.Create(request);
        }


        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static Task<OpenAIHttpOperationResult<ChatCompletionResponse, ErrorResponse>> Get(this IChatCompletionService chatCompletion, Message message, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(message);
            options?.Invoke(request);
            return chatCompletion.Create(request);
        }


        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static IAsyncEnumerable<OpenAIHttpOperationResult<ChatStreamCompletionResponse, ErrorResponse>> GetStream(this IChatCompletionService chatCompletion, string userMessage, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(Message.Create(ChatRoleType.User, userMessage));
            options?.Invoke(request);
            return chatCompletion.CreateStream(request);
        }

        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static IAsyncEnumerable<OpenAIHttpOperationResult<ChatStreamCompletionResponse, ErrorResponse>> GetStream(this IChatCompletionService chatCompletion,IList<Message> messages, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(messages);
            options?.Invoke(request);
            return chatCompletion.CreateStream(request);
        }


        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public static IAsyncEnumerable<OpenAIHttpOperationResult<ChatStreamCompletionResponse, ErrorResponse>> GetStream(this IChatCompletionService chatCompletion, Message message, Action<ChatCompletionRequest>? options = null)
        {
            var request = new ChatCompletionRequest(message);
            options?.Invoke(request);
            return chatCompletion.CreateStream(request);
        }
    }
}
