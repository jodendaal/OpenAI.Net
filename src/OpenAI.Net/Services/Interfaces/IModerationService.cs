using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Given a input text, outputs if the model classifies it as violating OpenAI's content policy. <br />
    /// Related guide: <a href="https://beta.openai.com/docs/guides/moderation">Moderations</a> 
    /// </summary>
    public interface IModerationService
    {
        /// <summary>
        /// <inheritdoc cref="IModerationService"/>
        /// </summary>
        Task<OpenAIHttpOperationResult<ModerationResponse, ErrorResponse>> Create(ModerationRequest model);
    }
}