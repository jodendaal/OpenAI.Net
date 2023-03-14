using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class CreateTranslationRequest
    {
        public CreateTranslationRequest(FileContentInfo file, string model = ModelTypes.Whisper1)
        {
            File = file;
            Model = model;
        }

        /// <summary>
        /// The audio file to transcribe, in one of these formats: mp3, mp4, mpeg, mpga, m4a, wav, or webm. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-file" />
        /// </summary>
        [Required]

        public FileContentInfo File { get; set; }

        // <summary>
        /// ID of the model to use. Only whisper-1 is currently available. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-model" />
        ///</summary>
        [Required]
        public string Model { get; set; }

        // <summary>
        /// An optional text to guide the model's style or continue a previous audio segment. The <a href="https://platform.openai.com/docs/guides/speech-to-text/prompting">prompt</a> should match the audio language. <br/>
        /// The prompt should be in English. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-prompt" />
        ///</summary>
        public string? Prompt { get; set; }

        // <summary>
        /// The format of the transcript output, in one of these options: json, text, srt, verbose_json, or vtt. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-response_format" />
        ///</summary>

        [JsonPropertyName("response_format")]
        public string? ResponseFormat { get; set; }

        // <summary>
        /// The sampling temperature, between 0 and 1. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic. If set to 0, the model will use <a href="https://en.wikipedia.org/wiki/Log_probability">log probability</a> to automatically increase the temperature until certain thresholds are hit. <br/>
        /// <see href="ht
        public double? Temperature { get; set; }
    }
}
