using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Services.Interfaces
{
    public interface IImageService
    {
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(ImageEditRequest request);
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Genearate(ImageGenerationRequest request);
        Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Variation(ImageVariationRequest request);
    }
}