using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Brokers;

namespace OpenAI.Net.Services
{
    /// <summary>
    /// <inheritdoc cref="IEmbeddingsService"/>
    /// </summary>
    public class EmbeddingsService : IEmbeddingsService
    {
        private readonly IEmbeddingsBroker _embeddingsBroker;

        /// <summary>
        /// <inheritdoc cref="IEmbeddingsService"/>
        /// </summary>
        public EmbeddingsService(IEmbeddingsBroker embeddingsBroker) 
        {
            _embeddingsBroker = embeddingsBroker;
        }

        public Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model)
        {
            return _embeddingsBroker.Create(model);
        }
    }
}
