using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Extensions;
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
            return HttpClient.OperationGetResult<FileListResponse, ErrorResponse>("v1/files");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Upload(UploadFileRequest request)
        {
            return HttpClient.OperationPostFormResult<FileInfoResponse, ErrorResponse>("v1/files", request);
        }

        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string fileId)
        {
            return HttpClient.OperationDeleteResult<DeleteResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Get(string fileId)
        {
            return HttpClient.OperationGetResult<FileInfoResponse, ErrorResponse>($"v1/files/{fileId}");
        }

        public Task<OpenAIHttpOperationResult<FileContentInfo, ErrorResponse>> GetContent(string fileId)
        {
            return HttpClient.OperationGetFileResult<ErrorResponse>($"v1/files/{fileId}/content");
        }
    }
}
