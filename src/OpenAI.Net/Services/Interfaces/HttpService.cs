using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IHttpServiceFactory
    {
        IHttpService Create();
    }

    public class HttpServiceFactory : IHttpServiceFactory
    {
        public HttpServiceFactory()
        {

        }

        public IHttpService Create()
        {
            return null;
        }
    }

    public class HttpServiceFactoryAzure : IHttpServiceFactory
    {
        private readonly AzureOpenAIConfig _config;

        public HttpServiceFactoryAzure(AzureOpenAIConfig config)
        {
            _config = config;
        }

        public IHttpService Create()
        {
            return null;
        }
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OpenAIHttpOperationResult<T, TError>> PostForm<T, TError>(string? path, Object @object)
        {
            return await ErrorHandler(async () =>
            {
                @object.Validate();
                var formData = @object.ToMultipartFormDataContent();

                var response = await _httpClient.PostAsync(path, formData);
                return await response.HandleResponse<T, TError>();
            });
        }

        public async Task<OpenAIHttpOperationResult<T, TError>> Delete<T, TError>(string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await _httpClient.DeleteAsync(path);
                return await response.HandleResponse<T, TError>();
            });
        }

        public async Task<OpenAIHttpOperationResult<T, TError>> Get<T, TError>(string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await _httpClient.GetAsync(path);
                return await response.HandleResponse<T, TError>();
            });
        }

        public async Task<OpenAIHttpOperationResult<FileContentInfo, TError>> GetFile<TError>(string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await _httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    var fileName = response.Content?.Headers?.ContentDisposition?.FileName?.Replace(@"""", "");
                    var fileContents = new FileContentInfo(bytes, fileName ?? "file");
                    return new OpenAIHttpOperationResult<FileContentInfo, TError>(fileContents, response.StatusCode);
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return new OpenAIHttpOperationResult<FileContentInfo, TError>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
            });
        }

        public async Task<OpenAIHttpOperationResult<T, TError>> Post<T, TError>(string? path, Object @object)
        {
            return await ErrorHandler(async () =>
            {
                @object.Validate();
                var response = await _httpClient.PostAsJsonAsync(path, @object, _jsonSerializerOptions);
                return await response.HandleResponse<T, TError>();
            });
        }

        public async IAsyncEnumerable<OpenAIHttpOperationResult<T, TError>> PostStream<T, TError>(string? path, Object @object)
        {
            @object.Validate();

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, path))
            {
                req.Content = new StringContent(JsonSerializer.Serialize(@object, _jsonSerializerOptions), UnicodeEncoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    using var reader = new StreamReader(responseStream);
                    string? line = null;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.StartsWith("data: "))
                            line = line.Substring("data: ".Length);

                        if (!string.IsNullOrWhiteSpace(line) && line != "[DONE]")
                        {
                            var t = JsonSerializer.Deserialize<T>(line.Trim(), _jsonSerializerOptions);
                            yield return new OpenAIHttpOperationResult<T, TError>(t, response.StatusCode);
                        }
                    }
                }
            }
        }

        public async Task<OpenAIHttpOperationResult<T, TError>> HandleResponse<T, TError>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseObject = await response.Content.ReadFromJsonAsync<T>();
                return new OpenAIHttpOperationResult<T, TError>(responseObject, response.StatusCode);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return new OpenAIHttpOperationResult<T, TError>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
        }

        private async Task<OpenAIHttpOperationResult<T, TError>> ErrorHandler<T, TError>(Func<Task<OpenAIHttpOperationResult<T, TError>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
