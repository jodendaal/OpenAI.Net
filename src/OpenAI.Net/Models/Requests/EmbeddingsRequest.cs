using System.ComponentModel.DataAnnotations;

namespace OpenAI.Net.Models.Requests
{
    public class EmbeddingsRequest
    {
        public EmbeddingsRequest(IList<string> input, string model)
        {
            Model = model;
            Input = input;
        }

        public EmbeddingsRequest(string input, string model) : this(input.ToList(), model) { }
       

        /// <summary>
        /// ID of the model to use. <br />
        /// You can use the List models API to see all of your available models, or see our Model overview for descriptions of them. <br />
        /// <see href="https://platform.openai.com/docs/api-reference/embeddings#embeddings/create-model" />
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Input text to get embeddings for, encoded as a string or array of tokens. <br />
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays. <br />
        /// Each input must not exceed 8192 tokens in length. <br />
        /// <see href="https://platform.openai.com/docs/api-reference/embeddings/create#embeddings/create-input" />
        /// </summary>
        [Required]
        public IList<string> Input { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <br/>
        /// <a href="https://platform.openai.com/docs/api-reference/images/create#images/create-user">Learn more</a>. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/embeddings/create#embeddings/create-user" />
        /// </summary>
        public string? User { get; set; }
    }
}
