using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Get a vector representation of a given input that can be easily consumed by machine learning models and algorithms. <br />
    /// Related guide: <a href="https://platform.openai.com/docs/guides/embeddings">Embeddings</a> 
    /// </summary>
    public interface IEmbeddingsService
    {
        /// <summary>
        /// Creates an embedding vector representing the input text.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(EmbeddingsRequest model);
    }
}