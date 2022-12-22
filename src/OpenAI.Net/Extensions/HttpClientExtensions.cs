using OpenAI.Net.Models.OperationResult;
using System.Net.Http.Json;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace OpenAI.Net.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<OpenAIHttpOperationResult<T,TError>> OperationPostResult<T, TError>(this HttpClient httpClient,string? path, Object @object, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            try
            {
                Validate(@object);
                var response = await httpClient.PostAsJsonAsync(path, @object, jsonSerializerOptions);
                if (response.IsSuccessStatusCode)
                {
                    var responseObject = await response.Content.ReadFromJsonAsync<T>();
                    return new OpenAIHttpOperationResult<T, TError>(responseObject, response.StatusCode);
                   
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return new OpenAIHttpOperationResult<T, TError>(new Exception(response.StatusCode.ToString(),new Exception(errorResponse)), response.StatusCode, errorResponse);
            }
            catch (Exception ex)
            {
                return new OpenAIHttpOperationResult<T, TError>(ex, System.Net.HttpStatusCode.BadRequest);
            }
        }

        private static void Validate(object @object)
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
