using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Extensions;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class FineTuneService : BaseService, IFineTuneService
    {
        public FineTuneService(HttpClient client) : base(client)
        {
        }
        public Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Create(FineTuneRequest request)
        {
            return HttpClient.Post<FineTuneResponse, ErrorResponse>($"v1/fine-tunes", request, JsonSerializerOptions);
        }

        public Task<OpenAIHttpOperationResult<FineTuneGetResponse, ErrorResponse>> Get()
        {
            return HttpClient.Get<FineTuneGetResponse, ErrorResponse>($"v1/fine-tunes");
        }

        public Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Get(string fineTuneId)
        {
            return HttpClient.Get<FineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}");
        }

        public Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Cancel(string fineTuneId)
        {
            return HttpClient.Get<FineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/cancel");
        }

        public Task<OpenAIHttpOperationResult<FineTuneEventsResponse, ErrorResponse>> GetEvents(string fineTuneId)
        {
            return HttpClient.Get<FineTuneEventsResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/events");
        }

        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string model)
        {
            return HttpClient.Delete<DeleteResponse, ErrorResponse>($"v1/models/{model}");
        }

    }
}
