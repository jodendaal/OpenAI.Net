using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Brokers
{
    public class TextCompletionBroker : ITextCompletionBroker
    {
        private readonly IHttpService _httpService;

        public TextCompletionBroker(IHttpService httpService) 
        {
            _httpService = httpService;
        }

        public Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request)
        {
            return _httpService.Post<TextCompletionResponse, ErrorResponse>("v1/completions", request);
        }

        public IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request)
        {
            return _httpService.PostStream<TextCompletionResponse, ErrorResponse>("v1/completions", request);
        }
    }
}
