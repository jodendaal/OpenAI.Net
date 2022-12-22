namespace OpenAI.Net.Models.OperationResult
{
    public class OperationResult<T>
    {
        public OperationResult() { }
        public OperationResult(T? result)
        {
            Result = result;
        }

        public OperationResult(Exception exception, string? errorMessaage = null)
        {
            Exception = exception;
            ErrorMessage = errorMessaage ?? exception.Message;
        }
        public T? Result { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public bool IsSuccess => Exception == null;
    }
}
