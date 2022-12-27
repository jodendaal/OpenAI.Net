using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public static class ImageServiceExtensionMethods
    {
        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Generate(this IImageService service, string prompt, int noOfImages = 1 ,string size = "1024x1024",Action<ImageGenerationRequest>? options = null)
        {
            var request = new ImageGenerationRequest(prompt) { Size = size,N=noOfImages };
            options?.Invoke(request);
            return service.Generate(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Generate(this IImageService service, string prompt, Action<ImageGenerationRequest>? options = null)
        {
            var request = new ImageGenerationRequest(prompt);
            options?.Invoke(request);
            return service.Generate(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(this IImageService service, string prompt,string imagePath, Action<ImageEditRequest>? options = null)
        {
            var inputImage = FileContentInfo.Load(imagePath);
            var request = new ImageEditRequest(prompt, inputImage);
            options?.Invoke(request);
            return service.Edit(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(this IImageService service, string prompt, byte[] imageBytes, Action<ImageEditRequest>? options = null)
        {
            var inputImage = new FileContentInfo(imageBytes,"file");
            var request = new ImageEditRequest(prompt, inputImage);
            options?.Invoke(request);
            return service.Edit(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(this IImageService service, string prompt, string imagePath,string maskImagePath, Action<ImageEditRequest>? options = null)
        {
            var inputImage = FileContentInfo.Load(imagePath);
            var maskImage = FileContentInfo.Load(maskImagePath);
            var request = new ImageEditRequest(prompt, inputImage) { Mask = maskImage };
            options?.Invoke(request);
            return service.Edit(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Edit(this IImageService service, string prompt, byte[] imageBytes, byte[] maskBytes, Action<ImageEditRequest>? options = null)
        {
            var inputImage = new FileContentInfo(imageBytes, "image");
            var maskImage = new FileContentInfo(maskBytes, "maskImage");
            var request = new ImageEditRequest(prompt, inputImage) { Mask = maskImage };
            options?.Invoke(request);
            return service.Edit(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Variation(this IImageService service, string imagePath, Action<ImageVariationRequest>? options = null)
        {
            var inputImage = FileContentInfo.Load(imagePath);
            var request = new ImageVariationRequest(inputImage);
            options?.Invoke(request);
            return service.Variation(request);
        }

        public static Task<OpenAIHttpOperationResult<ImageGenerationResponse, ErrorResponse>> Variation(this IImageService service, byte[] imageBytes, Action<ImageVariationRequest>? options = null)
        {
            var inputImage = new FileContentInfo(imageBytes, "file");
            var request = new ImageVariationRequest(inputImage);
            options?.Invoke(request);
            return service.Variation(request);
        }
    }
}
