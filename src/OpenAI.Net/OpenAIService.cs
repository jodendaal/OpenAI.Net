using OpenAI.Net.Extensions;
using OpenAI.Net.Models;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace OpenAI.Net
{
    public class OpenAIService : IOpenAIService
    {
        public IModelsService Models { get; }
        public ITextCompletionService TextCompletion { get; }

        public ITextEditService TextEdit { get; }
        public IImageService Images { get; }
        public IFilesService Files { get; }
        public IFineTuneService FineTune { get; }
        public IModerationService Moderation { get; }
        public IEmbeddingsService Embeddings { get; }

        public OpenAIService(IModelsService modelsService, ITextCompletionService textCompletionService, ITextEditService textEditService,IImageService imageService, IFilesService filesService,IFineTuneService fineTuneService,IModerationService moderationService, IEmbeddingsService embeddings)
        {
            Models = modelsService;
            TextCompletion = textCompletionService;
            TextEdit = textEditService;
            Images = imageService;
            Files = filesService;
            FineTune = fineTuneService;
            Moderation = moderationService;
            Embeddings = embeddings;
        }
    }
}
