using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class ChatCompletionService : BaseService, IChatCompletionService
    {
        public ChatCompletionService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<ChatCompletionResponse, ErrorResponse>> Create(ChatCompletionRequest request)
        {
            return HttpClient.Post<ChatCompletionResponse, ErrorResponse>("v1/chat/completions", request, JsonSerializerOptions);
        }

        public IAsyncEnumerable<OpenAIHttpOperationResult<ChatStreamCompletionResponse, ErrorResponse>> CreateStream(ChatCompletionRequest request)
        {
            request.Stream = true;
            return HttpClient.PostStream<ChatStreamCompletionResponse, ErrorResponse>("v1/chat/completions", request, JsonSerializerOptions);
        }
    }
}
