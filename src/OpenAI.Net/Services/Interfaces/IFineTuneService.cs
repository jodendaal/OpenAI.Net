using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IFineTuneService
    {
        Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Cancel(string fineTuneId);
        Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Create(FineTuneRequest request);
        Task<OpenAIHttpOperationResult<DeleteResponse, ErrorResponse>> Delete(string model);
        Task<OpenAIHttpOperationResult<FineTuneGetResponse, ErrorResponse>> Get();
        Task<OpenAIHttpOperationResult<FineTuneResponse, ErrorResponse>> Get(string fineTuneId);
        Task<OpenAIHttpOperationResult<FineTuneEventsResponse, ErrorResponse>> GetEvents(string fineTuneId);
    }
}