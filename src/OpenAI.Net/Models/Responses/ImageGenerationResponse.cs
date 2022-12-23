using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
