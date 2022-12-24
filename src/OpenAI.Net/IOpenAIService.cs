using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public interface IOpenAIService
    {
        public IModelsService Models { get; }
        public ITextCompletionService TextCompletion { get; }
        public ITextEditService TextEdit { get; }
        public IImageService Images { get; }
        public IFilesService Files { get; }
        public IFineTuneService FineTune { get; }
        public IModerationService Moderation { get; }
        public IEmbeddingsService Embeddings { get; }
    }
}