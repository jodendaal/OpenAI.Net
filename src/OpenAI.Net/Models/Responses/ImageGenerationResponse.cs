namespace OpenAI.Net.Models.Responses
{
    public class ImageGenerationResponse
    {
        public int Created { get; set; }
        public ImageInfo[] Data { get; set; }
    }

    public class ImageInfo
    {
        public string Url { get; set; }
    }

}
