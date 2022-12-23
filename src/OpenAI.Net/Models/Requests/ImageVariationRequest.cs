using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class ImageVariationRequest
    {
        public ImageVariationRequest(FileContentInfo image)
        {
            Image = image;
        }

        /// <summary>
        /// The image to edit. Must be a valid PNG file, less than 4MB, and square. <br />
        /// If mask is not provided, image must have transparency, which will be used as the mask.  <br />
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-variation#images/create-variation-image" />
        /// </summary>
        [Required]
        public FileContentInfo Image { get; set; }

        /// <summary>
        /// The number of images to generate. Must be between 1 and 10. <br/>
        /// Defaults to 1 <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-variation#images/create-variation-n" />
        /// </summary>
        public int? N { get; set; }

        /// <summary>
        /// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-variation#images/create-variation-size" />
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The format in which the generated images are returned. Must be one of url or b64_json. <br/>
        /// Defaults to url <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-variation#images/create-variation-response_format" />
        /// </summary>
        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <br/>
        /// <a href="https://beta.openai.com/docs/api-reference/images/create#images/create-user">Learn more</a>. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-variation#images/create-variation-user" />
        /// </summary>
        public string User { get; set; }
    }
}
