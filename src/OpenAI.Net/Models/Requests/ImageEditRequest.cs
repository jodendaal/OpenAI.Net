using System.ComponentModel.DataAnnotations;

namespace OpenAI.Net.Models.Requests
{
    public class ImageEditRequest : ImageGenerationRequest
    {
        public ImageEditRequest(string prompt, FileContentInfo image):base(prompt)
        {
            Prompt = prompt;
            Image = image;
        }

        /// <summary>
        /// The image to edit. Must be a valid PNG file, less than 4MB, and square. <br />
        /// If mask is not provided, image must have transparency, which will be used as the mask.  <br />
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-edit#images/create-edit-image" />
        /// </summary>
        [Required]
        public FileContentInfo Image { get; set; }

        /// <summary>
        /// An additional image whose fully transparent areas (e.g. where alpha is zero) indicate where image should be edited.  <br />
        /// Must be a valid PNG file, less than 4MB, and have the same dimensions as image.  <br />
        /// <see href="https://beta.openai.com/docs/api-reference/images/create-edit#images/create-edit-mask" />
        /// </summary>
        public FileContentInfo Mask { get; set; }
    }
}
