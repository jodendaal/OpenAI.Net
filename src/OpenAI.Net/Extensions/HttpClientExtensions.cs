using OpenAI.Net.Models.OperationResult;
using System.Net.Http.Json;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Reflection;
using OpenAI.Net.Models;

namespace OpenAI.Net.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<OpenAIHttpOperationResult<T, TError>> OperationPostFormResult<T, TError>(this HttpClient httpClient, string? path, Object @object)
        {
            try
            {
                @object.Validate();
                var formData = @object.ToMultipartFormDataContent();

                var response = await httpClient.PostAsync(path, formData);
                return await response.HandleResponse<T, TError>();
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static MultipartFormDataContent ToMultipartFormDataContent(this object @object)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            foreach (var prop in @object.GetType().GetProperties())
            {
               formData.AddField(prop, @object);
            }

            return formData;
        }

        public static async Task<OpenAIHttpOperationResult<T, TError>> OperationDeleteResult<T, TError>(this HttpClient httpClient, string? path)
        {
            try
            {
                var response = await httpClient.DeleteAsync(path);
                return await response.HandleResponse<T, TError>();
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<OpenAIHttpOperationResult<T, TError>> OperationGetResult<T, TError>(this HttpClient httpClient, string? path)
        {
            try
            {
                var response = await httpClient.GetAsync(path);
                return await response.HandleResponse<T, TError>();
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<OpenAIHttpOperationResult<FileContentInfo,TError>> OperationGetFileResult<TError>(this HttpClient httpClient, string? path)
        {
            try
            {
                var response = await httpClient.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    var fileName = response.Content?.Headers?.ContentDisposition?.FileName?.Replace(@"""","");
                    var fileContents = new FileContentInfo(bytes, fileName ?? "file");
                    return new OpenAIHttpOperationResult<FileContentInfo, TError>(fileContents, response.StatusCode);
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return new OpenAIHttpOperationResult<FileContentInfo, TError>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<FileContentInfo, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async Task<OpenAIHttpOperationResult<T,TError>> OperationPostResult<T, TError>(this HttpClient httpClient,string? path, Object @object, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            try
            {
                @object.Validate();
                var response = await httpClient.PostAsJsonAsync(path, @object, jsonSerializerOptions);
                return await response.HandleResponse<T, TError>();
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public static async IAsyncEnumerable<OpenAIHttpOperationResult<T, TError>> OperationPostStreamResult<T, TError>(this HttpClient httpClient, string? path, Object @object, JsonSerializerOptions? jsonSerializerOptions = null)
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

        public static void AddField(this MultipartFormDataContent formData, PropertyInfo prop,object @object)
        {
            var value = prop.GetValue(@object);

            if (value != null)
            {
                if (value is FileContentInfo)
                {
                    var fileInfo = value as FileContentInfo;
                    if (fileInfo != null)
                    {
                        formData.Add(fileInfo.FileContent.ToHttpContent(), prop.GetPropertyName(), $"@{fileInfo.FileName}");
                    }
                }
                else
                {
                    formData.Add(value.ToHttpContent(), prop.GetPropertyName());
                }
            }
        }

        public static string GetPropertyName(this PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (attribute != null)
            {
                return attribute.Name;
            }

            return prop.Name.ToLowerInvariant();
        }

        public static HttpContent ToHttpContent(this object value)
        {
            if (value is byte[])
            {
                return new ByteArrayContent((byte[])value);
            }

            return new StringContent(value?.ToString() ?? "");
        }

        public static void Validate(this object @object)
        {
            ICollection<ValidationResult> validationErrors = new List<ValidationResult>();
            Validator.TryValidateObject(@object, new ValidationContext(@object), validationErrors);
            if (validationErrors.Count > 0)
            {
                throw new ArgumentException(string.Join(Environment.NewLine,validationErrors));
            }
        }
    }
}
