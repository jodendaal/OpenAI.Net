using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class TextEditService : BaseService, ITextEditService
    {
        public TextEditService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<TextEditResponse, ErrorResponse>> Get(TextEditRequest request)
        {
            return HttpClient.Post<TextEditResponse, ErrorResponse>("v1/edits", request, JsonSerializerOptions);
        }
    }
}
