using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IFineTuneService
    {
        Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Cancel(string fineTuneId);
        Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Create(CreateFineTuneRequest request);
        Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string model);
        Task<OpenAIHttpOperationResult<GetFineTuneResponse, ErrorResponse>> Get();
        Task<OpenAIHttpOperationResult<CreateFineTuneResponse, ErrorResponse>> Get(string fineTuneId);
        Task<OpenAIHttpOperationResult<GetFineTuneEventsResponse, ErrorResponse>> GetEvents(string fineTuneId);
    }
}