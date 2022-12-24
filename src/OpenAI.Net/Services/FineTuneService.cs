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
        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Create(CreateFineTuneRequest request)
        {
            return HttpClient.OperationPostResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes", request, JsonSerializerOptions);
        }

        public Task<OpenAIHttpOperationResult<GetFineTuneResponse, ErrorResponse>> Get()
        {
            return HttpClient.OperationGetResult<GetFineTuneResponse, ErrorResponse>($"v1/fine-tunes");
        }

        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Get(string fineTuneId)
        {
            return HttpClient.OperationGetResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}");
        }

        public Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Cancel(string fineTuneId)
        {
            return HttpClient.OperationGetResult<CreateFineTuneResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/cancel");
        }

        public Task<OpenAIHttpOperationResult<GetFineTuneEventsResponse, ErrorResponse>> GetEvents(string fineTuneId)
        {
            return HttpClient.OperationGetResult<GetFineTuneEventsResponse, ErrorResponse>($"v1/fine-tunes/{fineTuneId}/events");
        }

        public Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string model)
        {
            return HttpClient.OperationDeleteResult<DeleteResponse, ErrorResponse>($"v1/models/{model}");
        }

    }
}
