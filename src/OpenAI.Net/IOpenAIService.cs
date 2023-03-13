using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net
{
    public interface IOpenAIService
    {

        /// <summary>
        /// <inheritdoc cref="IModelsService"/>
        /// </summary>
        public IModelsService Models { get; }

        /// <summary>
        /// <inheritdoc cref="ITextCompletionService"/>
        /// </summary>
        public ITextCompletionService TextCompletion { get; }

        /// <summary>
        /// <inheritdoc cref="ITextEditService"/>
        /// </summary>
        public ITextEditService TextEdit { get; }

        /// <summary>
        /// <inheritdoc cref="IImageService"/>
        /// </summary>
        public IImageService Images { get; }

        /// <summary>
        /// <inheritdoc cref="IFilesService"/>
        /// </summary>
        public IFilesService Files { get; }

        /// <summary>
        /// <inheritdoc cref="IFineTuneService"/>
        /// </summary>
        public IFineTuneService FineTune { get; }

        /// <summary>
        /// <inheritdoc cref="IModerationService"/>
        /// </summary>
        public IModerationService Moderation { get; }

        /// <summary>
        /// <inheritdoc cref="IEmbeddingsService"/>
        /// </summary>
        public IEmbeddingsService Embeddings { get; }

        /// <summary>
        /// <inheritdoc cref="IChatCompletionService"/>
        /// </summary>
        public IChatCompletionService Chat { get;}
    }
}