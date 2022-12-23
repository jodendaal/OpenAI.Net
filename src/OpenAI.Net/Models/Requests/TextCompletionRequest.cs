using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class TextCompletionRequest
    {
        public TextCompletionRequest(string model, string prompt)
        {
            Model = model;
            Prompt = prompt;
        }

        /// <summary>
        /// ID of the model to use.<br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-model" />
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays. <br/>
        /// Note that<|endoftext|> is the document separator that the model sees during training, so if a prompt is not specified the model will generate as if from the beginning of a new document.<br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-prompt" />
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The maximum number of <a href="https://beta.openai.com/tokenizer">tokens</a> to generate in the completion. <br/> 
        ///  The token count of your prompt plus max_tokens cannot exceed the model's context length. Most models have a context length of 2048 tokens (except for the newest models, which support 4096). <br/>
        ///  <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-max_tokens" />
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 16;

        /// <summary>
        /// The suffix that comes after a completion of inserted text. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-suffix" />
        /// </summary>
        public string? Suffix { get; set; }

        /// <summary>
        /// What <a href="https://towardsdatascience.com/how-to-sample-from-language-models-682bceb97277">sampling temperature</a> to use. Higher values means the model will take more risks. <br/>
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. <br/>
        /// We generally recommend altering this or top_p but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-temperature" />
        /// </summary>
        public int Temperature { get; set; } = 1;

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. <br/>
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered. <br/>
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public int TopP { get; set; } = 1;

        /// <summary>
        /// How many completions to generate for each prompt. <br/>
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. <br/>
        /// Use carefully and ensure that you have reasonable settings for max_tokens and stop. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-n" />
        /// </summary>
        public int N { get; set; } = 1;


        /// <summary>
        /// Whether to stream back partial progress.  <br/>
        /// If set, tokens will be sent as data-only <a href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format">server-sent</a> events as they become available, with the stream terminated by a data: [DONE] message.  <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-stream" />
        /// </summary>
        public bool Stream { get; set; }

        /// <summary>
        /// Include the log probabilities on the logprobs most likely tokens, as well the chosen tokens. <br/>
        /// For example, if logprobs is 5, the API will return a list of the 5 most likely tokens. <br/>
        /// The API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response. <br/>
        /// The maximum value for logprobs is 5. If you need more than this, please contact us through our  <a href="https://help.openai.com/">Help center</a> and describe your use case. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-logprobs" />
        /// </summary>
        [JsonPropertyName("logprobs")]
        public int? LogProbs { get; set; }

        /// <summary>
        /// Echo back the prompt in addition to the completion <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-echo" />
        /// </summary>
        public bool? Echo { get; set; } 

        /// <summary>
        /// Up to 4 sequences where the API will stop generating further tokens. <br/>
        /// The returned text will not contain the stop sequence. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-stop" />
        /// </summary>
        public string? Stop { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. <br/>
        /// Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim. <br/>
        /// <a href="https://beta.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</a> <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-frequency_penalty" />
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public float FrequencyPenalty { get; set; } = 0;

        /// <summary>
        /// Generates best_of completions server-side and returns the "best" (the one with the highest log probability per token). <br/>
        /// Results cannot be streamed.<br/>
        /// When used with n, best_of controls the number of candidate completions and n specifies how many to return – best_of must be greater than n. <br/>
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. <br/>
        /// Use carefully and ensure that you have reasonable settings for max_tokens and stop. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-best_of" />
        /// </summary>
        [JsonPropertyName("best_of")]
        public int? BestOf { get; set; }

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion. <br/>
        /// Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias value from -100 to 100. <br/>
        /// You can use this <a href="https://beta.openai.com/tokenizer?view=bpe"> tokenizer tool </a> (which works for both GPT-2 and GPT-3) to convert text to token IDs. <br/>
        /// Mathematically, the bias is added to the logits generated by the model prior to sampling. <br/>
        /// The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the relevant token. <br/>
        /// As an example, you can pass  <br/>
        /// to prevent the  <![CDATA[<|endoftext|>]]>  token from being generated. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-logit_bias" />
        /// </summary>
        [JsonPropertyName("logit_bias")]
        public object? LogitBias { get; set; } = null;

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <a href="https://beta.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</a>.
        /// </summary>
        public string User { get; set; }
    }
}
