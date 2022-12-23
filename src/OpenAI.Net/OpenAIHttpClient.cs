using OpenAI.Net.Extensions;
using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
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

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> ImageGeneration(ImageGenerationRequest request)
        {
            return _httpClient.OperationPostResult<ImageGenerationResponse, ErrorResponse>("v1/images/generations", request, GetJsonSerializerOptions());
        }

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> ImageEdit(ImageEditRequest request)
        {
            return _httpClient.OperationPostFormResult<ImageGenerationResponse, ErrorResponse>("v1/images/edits", request);
        }

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> ImageVariation(ImageVariationRequest request)
        {
            return _httpClient.OperationPostFormResult<ImageGenerationResponse, ErrorResponse>("v1/images/variations", request);
        }

        public Task<OpenAIHttpOperationResult<FileListResponse, ErrorResponse>> GetFiles()
        {
            return _httpClient.OperationGetResult<FileListResponse, ErrorResponse>("v1/files");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> UploadFile(UploadFileRequest request )
        {
            return _httpClient.OperationPostFormResult<FileInfoResponse, ErrorResponse>("v1/files",request);
        }
        
        public Task<OpenAIHttpOperationResult<DeleteFileResponse, ErrorResponse>> DeleteFile(string fileId)
        {
            return _httpClient.OperationDeleteResult<DeleteFileResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> RetrieveFile(string fileId)
        {
            return _httpClient.OperationGetResult<FileInfoResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileContentInfo, ErrorResponse>> RetrieveFileContent(string fileId)
        {
            return _httpClient.OperationGetFileResult<ErrorResponse>($"v1/files/{fileId}/content");
        }


        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

      
    }
}
