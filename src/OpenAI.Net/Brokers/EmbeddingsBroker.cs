using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Brokers
{
    public class EmbeddingsBroker : IEmbeddingsBroker
    {

        private readonly IHttpService _httpService;

        public EmbeddingsBroker(IHttpService httpService)
        {
            _httpService = httpService;
        }


        public Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model)
        {
            return _httpService.Post<EmbeddingsResponse, ErrorResponse>($"v1/embeddings", model);
        }
    }
}
