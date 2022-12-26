using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    /// <summary>
    /// <inheritdoc cref="IEmbeddingsService"/>
    /// </summary>
    public class EmbeddingsService : BaseService, IEmbeddingsService
    {
        /// <summary>
        /// <inheritdoc cref="IEmbeddingsService"/>
        /// </summary>
        public EmbeddingsService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model)
        {
            return HttpClient.Post<EmbeddingsResponse, ErrorResponse>($"v1/embeddings", model, JsonSerializerOptions);
        }

        public Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsListRequest model)
        {
            return HttpClient.Post<EmbeddingsResponse, ErrorResponse>($"v1/embeddings", model, JsonSerializerOptions);
        }
    }
}
