using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Given a prompt and an instruction, the model will return an edited version of the prompt.
    /// </summary>
    public interface ITextEditService
    {
        /// <summary>
        /// <inheritdoc cref="ITextEditService"/>
        /// </summary>
        Task<OpenAIHttpOperationResult<TextEditResponse, ErrorResponse>> Get(TextEditRequest request);
    }
}