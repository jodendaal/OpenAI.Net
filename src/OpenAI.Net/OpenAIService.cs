using OpenAI.Net.Services.Interfaces;

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
        public IChatCompletionService Chat { get; }

        public OpenAIService(
            IModelsService modelsService, 
            ITextCompletionService textCompletionService,
            ITextEditService textEditService,
            IImageService imageService,
            IFilesService filesService,
            IFineTuneService fineTuneService,
            IModerationService moderationService, 
            IEmbeddingsService embeddings,
            IChatCompletionService chat)
        {
            Models = modelsService;
            TextCompletion = textCompletionService;
            TextEdit = textEditService;
            Images = imageService;
            Files = filesService;
            FineTune = fineTuneService;
            Moderation = moderationService;
            Embeddings = embeddings;
            Chat = chat;
        }
    }
}
