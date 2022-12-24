using OpenAI.Net.Services.Interfaces;
using System.Text.Json;

namespace OpenAI.Net.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public HttpClient HttpClient { get { return _httpClient; } }
        public JsonSerializerOptions JsonSerializerOptions { get { return _jsonSerializerOptions; } }
        public BaseService(HttpClient client)
        {
            _httpClient = client;
        }
    }
}
