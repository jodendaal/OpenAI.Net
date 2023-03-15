using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Brokers;

namespace OpenAI.Net.Services
{
    public class TextCompletionService : ITextCompletionService
    {
        private readonly ITextCompletionBroker _textCompletionBroker;

        public TextCompletionService(ITextCompletionBroker textCompletionBroker)
        {
            _textCompletionBroker = textCompletionBroker;
        }

        public Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(TextCompletionRequest request)
        {
            return _textCompletionBroker.Get(request);
        }

        public IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(TextCompletionRequest request)
        {
            request.Stream = true;
            return _textCompletionBroker.GetStream(request);
        }
    }
}
