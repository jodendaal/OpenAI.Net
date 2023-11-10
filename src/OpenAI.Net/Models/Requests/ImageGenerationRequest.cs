﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class ImageGenerationRequest
    {
        public ImageGenerationRequest(string prompt) 
        { 
            Prompt = prompt;
        }

        /// <summary>
        /// A text description of the desired image(s). The maximum length is 1000 characters <br />
        /// for dall-e-2 and 4000 characters for dall-e-3. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-prompt" />
        /// </summary>
        [Required]
        public string Prompt { get; set; }

        /// <summary>
        /// The model to use for image generation. <br/>
        /// Defaults to dall-e-2 <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-model" />
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The number of images to generate. Must be between 1 and 10. <br/>
        /// Defaults to 1 <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-n" />
        /// </summary>
        public int? N { get; set; }

        /// <summary>
        /// The quality of the image that will be generated. hd creates images with finer details <br />
        /// and greater consistency across the image. This param is only supported for dall-e-3. <br/>
        /// Defaults to standard <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-quality" />
        /// </summary>
        public string Quality { get; set; }

        /// <summary>
        /// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024 <br />
        /// for dall-e-2. Must be one of 1024x1024, 1792x1024, or 1024x1792 for dall-e-3 models. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-size" />
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The style of the generated images. Must be one of vivid or natural. Vivid causes <br />
        /// the model to lean towards generating hyper-real and dramatic images. Natural causes <br />
        /// the model to produce more natural, less hyper-real looking images. This param is only <br />
        /// supported for dall-e-3. <br/>
        /// Defaults to natural <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-style" />
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// The format in which the generated images are returned. Must be one of url or b64_json. <br/>
        /// Defaults to url <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-response_format" />
        /// </summary>
        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <br/>
        /// <a href="https://platform.openai.com/docs/api-reference/images/create#images-create-user">Learn more</a>. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/images/create#images-create-user" />
        /// </summary>
        public string User { get; set; }
    }
}
