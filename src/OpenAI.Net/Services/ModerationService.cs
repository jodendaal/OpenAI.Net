using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Extensions;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class ModerationService : BaseService, IModerationService
    {
        public ModerationService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<CreateModerationResponse, ErrorResponse>> Create(CreateModerationRequest model)
        {
            return HttpClient.OperationPostResult<CreateModerationResponse, ErrorResponse>($"v1/moderations", model, JsonSerializerOptions);
        }
    }
}
