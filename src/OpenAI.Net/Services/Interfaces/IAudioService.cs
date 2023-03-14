using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Turn audio into text
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// <inheritdoc cref="IAudioService"/>
        /// Transcribes audio into the input language.
        /// </summary>
        Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranscription(CreateTranscriptionRequest request);

        /// <summary>
        /// <inheritdoc cref="IAudioService"/>
        /// Translates audio into into English.
        /// </summary>
        Task<OpenAIHttpOperationResult<AudioReponse, ErrorResponse>> GetTranslation(CreateTranslationRequest request);
    }
}