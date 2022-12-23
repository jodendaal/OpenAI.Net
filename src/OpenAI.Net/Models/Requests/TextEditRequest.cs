using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class TextEditRequest
    {
        public TextEditRequest(string model, string instruction, string input = "")
        {
            Model = model;
            Instruction = instruction;
            Input = input;
        }

        /// <summary>
        /// The input text to use as a starting point for the edit. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-input" />
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// The instruction that tells the model how to edit the prompt. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-instruction" />
        /// </summary>
        [Required]
        public string Instruction { get; set; }

        /// <summary>
        /// ID of the model to use.<br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-model" />
        /// </summary>
        [Required]
        public string Model { get; set; }


        /// <summary>
        /// What <a href="https://towardsdatascience.com/how-to-sample-from-language-models-682bceb97277">sampling temperature</a> to use. Higher values means the model will take more risks. <br/>
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. <br/>
        /// We generally recommend altering this or top_p but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-temperature" />
        /// </summary>
        public int Temperature { get; set; } = 1;

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. <br/>
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered. <br/>
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public int TopP { get; set; } = 1;

        /// <summary>
        /// How many edits to generate for the input and instruction. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/edits/create#edits/create-n" />
        /// </summary>
        public int N { get; set; } = 1;
    }
}
