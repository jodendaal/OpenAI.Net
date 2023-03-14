using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class AudioServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranscription(this IAudioService audioService, string fileName, Action<CreateTranscriptionRequest>? options = null)
        {
            var request = new CreateTranscriptionRequest(FileContentInfo.Load(fileName));
            options?.Invoke(request);
            return audioService.GetTranscription(request);
        }

        public static Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranslation(this IAudioService audioService, string fileName, Action<CreateTranslationRequest>? options = null)
        {
            var request = new CreateTranslationRequest(FileContentInfo.Load(fileName));
            options?.Invoke(request);
            return audioService.GetTranslation(request);
        }
    }
}
