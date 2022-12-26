using OpenAI.Net.Models.OperationResult;
using System.Net.Http.Json;
using System.Text.Json;
using OpenAI.Net.Models;

namespace OpenAI.Net
{
    public static class HttpClientExtensions
    {
        public static async Task<OpenAIHttpOperationResult<T, TError>> PostForm<T, TError>(this HttpClient httpClient, string? path, Object @object)
        {
            return await ErrorHandler(async () =>
            {
                @object.Validate();
                var formData = @object.ToMultipartFormDataContent();

                var response = await httpClient.PostAsync(path, formData);
                return await response.HandleResponse<T, TError>();
            });
        }

        public static async Task<OpenAIHttpOperationResult<T, TError>> Delete<T, TError>(this HttpClient httpClient, string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await httpClient.DeleteAsync(path);
                return await response.HandleResponse<T, TError>();
            });
        }

        public static async Task<OpenAIHttpOperationResult<T, TError>> Get<T, TError>(this HttpClient httpClient, string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await httpClient.GetAsync(path);
                return await response.HandleResponse<T, TError>();
            });
        }

        public static async Task<OpenAIHttpOperationResult<FileContentInfo,TError>> GetFile<TError>(this HttpClient httpClient, string? path)
        {
            return await ErrorHandler(async () =>
            {
                var response = await httpClient.GetAsync(path);
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

        public static async Task<OpenAIHttpOperationResult<T, TError>> Post<T, TError>(this HttpClient httpClient, string? path, Object @object, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return await ErrorHandler(async () =>
            {
                @object.Validate();
                var response = await httpClient.PostAsJsonAsync(path, @object, jsonSerializerOptions);
                return await response.HandleResponse<T, TError>();
            });
        }

        public static async IAsyncEnumerable<OpenAIHttpOperationResult<T, TError>> PostStream<T, TError>(this HttpClient httpClient, string? path, Object @object, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            @object.Validate();
            var response = await httpClient.PostAsJsonAsync(path, @object, jsonSerializerOptions);
            
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
                        var t = JsonSerializer.Deserialize<T>(line.Trim(), jsonSerializerOptions);
                        yield return new OpenAIHttpOperationResult<T, TError>(t, response.StatusCode);
                    }
                }
            }
        }

        public static async Task<OpenAIHttpOperationResult<T, TError>> HandleResponse<T, TError>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseObject = await response.Content.ReadFromJsonAsync<T>();
                return new OpenAIHttpOperationResult<T, TError>(responseObject, response.StatusCode);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return new OpenAIHttpOperationResult<T, TError>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
        }

        private static async Task<OpenAIHttpOperationResult<T, TError>> ErrorHandler<T, TError>(Func<Task<OpenAIHttpOperationResult<T, TError>>> func)
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
