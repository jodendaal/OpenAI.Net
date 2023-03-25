using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public class OpenAIService : IOpenAIService
    {
        public IModelsService Models { get; }
        public ITextCompletionService TextCompletion { get; }

        public IImageService Images { get; }
        public IFilesService Files { get; }
        public IFineTuneService FineTune { get; }
        public IModerationService Moderation { get; }
        public IEmbeddingsService Embeddings { get; }
        public IChatCompletionService Chat { get; }
        public IAudioService Audio { get; }

        public OpenAIService(
            IModelsService modelsService, 
            ITextCompletionService textCompletionService,
            IImageService imageService,
            IFilesService filesService,
            IFineTuneService fineTuneService,
            IModerationService moderationService, 
            IEmbeddingsService embeddings,
            IChatCompletionService chat,
            IAudioService audio)
        {
            Models = modelsService;
            TextCompletion = textCompletionService;
            Images = imageService;
            Files = filesService;
            FineTune = fineTuneService;
            Moderation = moderationService;
            Embeddings = embeddings;
            Chat = chat;
            Audio = audio;
        }
    }
}
