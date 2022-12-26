using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;
using OpenAI.Net.Models;

namespace OpenAI.Net.Services
{
    public class FilesService : BaseService, IFilesService
    {
        public FilesService(HttpClient client) : base(client)
        {
        }


        public Task<OpenAIHttpOperationResult<FileListResponse, ErrorResponse>> Get()
        {
            return HttpClient.Get<FileListResponse, ErrorResponse>("v1/files");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Upload(FileUploadRequest request)
        {
            return HttpClient.PostForm<FileInfoResponse, ErrorResponse>("v1/files", request);
        }

        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string fileId)
        {
            return HttpClient.Delete<DeleteResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Get(string fileId)
        {
            return HttpClient.Get<FileInfoResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileContentInfo, ErrorResponse>> GetContent(string fileId)
        {
            return HttpClient.GetFile<ErrorResponse>($"v1/files/{fileId}/content");
        }
    }
}
