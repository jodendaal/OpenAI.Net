using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Extensions
{
    public static class FineTuneServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Create(this IFineTuneService service, string trainingFile, Action<FineTuneRequest>? options = null)
        {
            var request = new FineTuneRequest(trainingFile);
            options?.Invoke(request);
            return service.Create(request);
        }
    }
}
