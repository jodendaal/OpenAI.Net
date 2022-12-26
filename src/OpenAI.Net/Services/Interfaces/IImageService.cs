using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    /// <summary>
    /// Given a prompt and/or an input image, the model will generate a new image. <br/>
    /// Related guide: <a href="https://beta.openai.com/docs/guides/images">Image generation</a> 
    /// </summary>
    public interface IImageService
    {
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(ImageEditRequest request);
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Generate(ImageGenerationRequest request);
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Variation(ImageVariationRequest request);
    }
}