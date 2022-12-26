using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class TextEditServiceExtensionMethods
    {
        /// <summary>
        /// <inheritdoc cref="ITextEditService"/>
        /// </summary>
        public static Task<OpenAIHttpOperationResult<TextEditResponse, ErrorResponse>>Get(this ITextEditService service, string model, string instruction, string input, Action<TextEditRequest>? options = null)
        {
            var request = new TextEditRequest(model, instruction,input);
            options?.Invoke(request);
            return service.Get(request);
        }
    }
}
