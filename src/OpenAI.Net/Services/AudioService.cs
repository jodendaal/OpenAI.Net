using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Services
{
    public class AudioService : BaseService, IAudioService
    {
        public AudioService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranscription(CreateTranscriptionRequest request)
        {
            return HttpClient.PostForm<AudioReponse, ErrorResponse>("v1/audio/transcriptions", request);
        }

        public Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranslation(CreateTranslationRequest request)
        {
            return HttpClient.PostForm<AudioReponse, ErrorResponse>("v1/audio/translations", request);
        }
    }
}
