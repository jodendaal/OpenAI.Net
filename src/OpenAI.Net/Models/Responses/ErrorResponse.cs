namespace OpenAI.Net.Models.Responses
{
    public  class ErrorResponse
    {
        public Error Error { get; set; }
    }
    
    public class Error
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public object Param { get; set; }
        public object Code { get; set; }
    }
}
