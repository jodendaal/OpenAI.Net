using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Files are used to upload documents that can be used with features like <a href="https://beta.openai.com/docs/api-reference/fine-tunes">Fine-tuning</a>.
    /// </summary>
    public interface IFilesService
    {
        Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string fileId);
        Task<OpenAIHttpOperationResult<FileListResponse, ErrorResponse>> Get();
        Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Get(string fileId);
        Task<OpenAIHttpOperationResult<FileContentInfo, ErrorResponse>> GetContent(string fileId);
        Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Upload(FileUploadRequest request);
    }
}