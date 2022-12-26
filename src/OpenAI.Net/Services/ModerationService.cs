using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class ModerationService : BaseService, IModerationService
    {
        public ModerationService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<ModerationResponse, ErrorResponse>> Create(ModerationRequest model)
        {
            return HttpClient.Post<ModerationResponse, ErrorResponse>($"v1/moderations", model, JsonSerializerOptions);
        }

    }
}
