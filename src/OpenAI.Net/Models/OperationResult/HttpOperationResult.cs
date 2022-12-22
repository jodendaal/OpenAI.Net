using System.Net;

namespace OpenAI.Net.Models.OperationResult
{
    public class HttpOperationResult<T> : OperationResult<T>
    {
        public HttpOperationResult(T result, HttpStatusCode httpStatusCode) : base(result)
        {
            StatusCode = httpStatusCode;
        }

        public HttpOperationResult(Exception exception, HttpStatusCode httpStatusCode, string? errorMessaage = null) : base(exception, errorMessaage)
        {
            StatusCode = httpStatusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
