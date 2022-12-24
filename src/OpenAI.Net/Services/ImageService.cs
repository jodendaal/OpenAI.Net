using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Extensions;
using OpenAI.Net.Services.Interfaces;
using System.Net.Http;

namespace OpenAI.Net.Services
{
    public class ImageService : BaseService, IImageService
    {
        public ImageService(HttpClient client) : base(client)
        {
        }

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Genearate(ImageGenerationRequest request)
        {
            return HttpClient.Post<ImageGenerationResponse, ErrorResponse>("v1/images/generations", request, JsonSerializerOptions);
        }

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(ImageEditRequest request)
        {
            return HttpClient.PostForm<ImageGenerationResponse, ErrorResponse>("v1/images/edits", request);
        }

        public Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Variation(ImageVariationRequest request)
        {
            return HttpClient.PostForm<ImageGenerationResponse, ErrorResponse>("v1/images/variations", request);
        }
    }
}
