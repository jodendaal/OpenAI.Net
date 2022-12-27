using OpenAI.Net.Models;

namespace OpenAI.Net
{
    public static class StringExtensions
    {
        public static IList<string> ToList(this string value) 
        { 
            return new List<string> { value };  
        }

        public static byte[] Base64ToBytes(this string value)
        {
            return Convert.FromBase64String(value);
        }

        public static FileContentInfo Base64ToFileContent(this string value,string fileName = "image.png")
        {
            return new FileContentInfo(value.Base64ToBytes(), fileName);
        }
    }
}
