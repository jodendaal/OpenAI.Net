using OpenAI.Net.Extensions;
using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using System.Reflection;
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
        public IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> TextCompletionStream(TextCompletionRequest request)
        {
            request.Stream = true;
            return _httpClient.OperationPostStreamResult<TextCompletionResponse, ErrorResponse>("v1/completions", request, GetJsonSerializerOptions());
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
        
        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> DeleteFile(string fileId)
        {
            return _httpClient.OperationDeleteResult<DeleteResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> RetrieveFile(string fileId)
        {
            return _httpClient.OperationGetResult<FileInfoResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileContentInfo, ErrorResponse>> RetrieveFileContent(string fileId)
        {
            return _httpClient.OperationGetFileResult<ErrorResponse>($"v1/files/{fileId}/content");
        }

        public Task<OpenAIHttpOperationResult<ModelsResponse, ErrorResponse>> GetModels()
        {
            return _httpClient.OperationGetResult<ModelsResponse,ErrorResponse>($"v1/models");
        }

        public Task<OpenAIHttpOperationResult<ModelInfo, ErrorResponse>> GetModel(string model)
        {
            return _httpClient.OperationGetResult<ModelInfo, ErrorResponse>($"v1/models/{model}");
        }

        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> CreateFineTune(CreateFineTuneRequest request)
        {
            return _httpClient.OperationPostResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes", request,GetJsonSerializerOptions());
        }

        public Task<OpenAIHttpOperationResult<GetFineTuneResponse, ErrorResponse>> GetFineTunes()
        {
            return _httpClient.OperationGetResult<GetFineTuneResponse, ErrorResponse>($"v1/fine-tunes");
        }

        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> GetFineTune(string fineTuneId)
        {
            return _httpClient.OperationGetResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}");
        }

        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> CancelFineTune(string fineTuneId)
        {
            return _httpClient.OperationGetResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/cancel");
        }

        public Task<OpenAIHttpOperationResult<GetFineTuneEventsResponse, ErrorResponse>> GetFineTuneEvents(string fineTuneId)
        {
            return _httpClient.OperationGetResult<GetFineTuneEventsResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/events");
        }

        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> DeleteFineTune(string model)
        {
            return _httpClient.OperationDeleteResult<DeleteResponse, ErrorResponse>($"v1/models/{model}");
        }

        public Task<OpenAIHttpOperationResult<CreateModerationResponse, ErrorResponse>> CreateModeration(CreateModerationRequest model)
        {
            return _httpClient.OperationPostResult<CreateModerationResponse, ErrorResponse>($"v1/moderations", model,GetJsonSerializerOptions());
        }

        public Task<OpenAIHttpOperationResult<CreateEmbeddingsResponse, ErrorResponse>> CreateEmbeddings(CreateEmbeddingsRequest model)
        {
            return _httpClient.OperationPostResult<CreateEmbeddingsResponse, ErrorResponse>($"v1/embeddings", model, GetJsonSerializerOptions());
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
