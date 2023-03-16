using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using System.Text.Json;

namespace OpenAI.Net.Brokers
{
    public interface IEmbeddingsBroker
    {
        Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model);
    }
}
