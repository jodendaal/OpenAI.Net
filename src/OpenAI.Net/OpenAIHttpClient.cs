using OpenAI.Net.Extensions;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using System.Text.Json;

namespace OpenAI.Net
{
    public class OpenAIHttpClient
    {
        private readonly HttpClient _httpClient;

        public OpenAIHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<OpenAIHttpOperationResult<TextCompletionResponse,ErrorResponse>> TextCompletion(TextCompletionRequest request)
        {
           return _httpClient.OperationPostResult<TextCompletionResponse,ErrorResponse>("v1/completions", request, GetJsonSerializerOptions());
        }

        public Task<OpenAIHttpOperationResult<TextEditResponse, ErrorResponse>> TextEdit(TextEditRequest request)
        {
            return _httpClient.OperationPostResult<TextEditResponse, ErrorResponse>("v1/edits", request, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
