using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class FilesServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Upload(this IFilesService service, string filePath,string purpose = "fine-tune")
        {
            var fileInfo = FileContentInfo.Load(filePath);
            var request = new FileUploadRequest(fileInfo);
            return service.Upload(request);
        }

        public static Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> UploadWithName(this IFilesService service, string filePath,string fileName, string purpose = "fine-tune")
        {
            var fileInfo = FileContentInfo.Load(filePath);
            fileInfo.FileName = fileName;
            var request = new FileUploadRequest(fileInfo);

            return service.Upload(request);
        }

        public static Task<OpenAIHttpOperationResult<FileInfoResponse, ErrorResponse>> Upload(this IFilesService service, byte[] filebytes, string fileName, string purpose = "fine-tune")
        {
            var fileInfo = new FileContentInfo(filebytes, fileName);
            var request = new FileUploadRequest(fileInfo);
            return service.Upload(request);
        }
    }
}
