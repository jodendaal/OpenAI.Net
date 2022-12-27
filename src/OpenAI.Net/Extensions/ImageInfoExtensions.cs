using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models;

namespace OpenAI.Net
{
    public static class ImageInfoExtensions
    {
        public static FileContentInfo Base64ToFileContent(this ImageInfo value, string fileName = "image.png")
        {
            return new FileContentInfo(value.Base64.Base64ToBytes(), fileName);
        }
    }
}
