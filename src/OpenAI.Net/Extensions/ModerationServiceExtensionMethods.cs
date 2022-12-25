using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Extensions
{
    public static class ModerationServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<ModerationResponse, ErrorResponse>> Create(this IModerationService service, string input,string model)
        {
            var request = new ModerationRequest(input) { Model = model };
            return service.Create(request);
        }
    }
}
