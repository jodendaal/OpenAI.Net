using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Extensions
{
    public static class EmbeddingsServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<EmbeddingsResponse, ErrorResponse>> Create(this IEmbeddingsService service, string input,string model,string? user = null)
        {
            var request = new EmbeddingsRequest(input, model) { User = user };
            return service.Create(request);
        }
    }
}
