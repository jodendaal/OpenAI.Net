﻿using Microsoft.VisualBasic;
using OpenAI.Net.Models.Responses;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{

    public class ChatCompletionRequest
    {
        public ChatCompletionRequest(Message message, string model = ModelTypes.GPT35Turbo)
            : this(model, message.ToList())
        {
        }

        public ChatCompletionRequest(IList<Message> messages, string model = ModelTypes.GPT35Turbo)
            :this(model,messages)
        {           
        }

        public ChatCompletionRequest(string model, IList<Message> messages)
        {
            Model = model;
            Messages = messages;
        }
        public ChatCompletionRequest(string model, Message message) : this(model,message.ToList()) { }

        /// <summary>
        /// The messages to generate chat completions for.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/chat/create#chat/create-messages" />
        /// </summary>
        public IList<Message> Messages { get; set; }

        /// <summary>
        /// ID of the model to use. Currently, only gpt-3.5-turbo and gpt-3.5-turbo-0301 are supported.<br/>
        /// <see href="https://platform.openai.com/docs/api-reference/chat/create#chat/create-model" />
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// The maximum number of <a href="https://beta.openai.com/tokenizer">tokens</a> to generate in the answer. <br/> 
        /// By default, the number of tokens the model can return will be (4096 - prompt tokens). <br/>
        ///  <see href="https://platform.openai.com/docs/api-reference/chat/create#chat/create-max_tokens" />
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 16;


        /// <summary>
        /// What <a href="https://towardsdatascience.com/how-to-sample-from-language-models-682bceb97277">sampling temperature</a> to use. Higher values means the model will take more risks. <br/>
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. <br/>
        /// We generally recommend altering this or top_p but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/chat/create#chat/create-temperature" />
        /// </summary>
        public double Temperature { get; set; } = 1;

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. <br/>
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered. <br/>
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public double TopP { get; set; } = 1;

        /// <summary>
        /// How many completions to generate for each prompt. <br/>
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. <br/>
        /// Use carefully and ensure that you have reasonable settings for max_tokens and stop. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-n" />
        /// </summary>
        public int? N { get; set; } = 1;


        /// <summary>
        /// Whether to stream back partial progress.  <br/>
        /// If set, tokens will be sent as data-only <a href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format">server-sent</a> events as they become available, with the stream terminated by a data: [DONE] message.  <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-stream" />
        /// </summary>
        public bool Stream { get; internal set; }

      

        /// <summary>
        /// Up to 4 sequences where the API will stop generating further tokens. <br/>
        /// The returned text will not contain the stop sequence. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-stop" />
        /// </summary>
        public IList<string>? Stop { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. <br/>
        /// Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim. <br/>
        /// <a href="https://platform.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</a> <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/chat/create#chat/create-presence_penalty" />
        /// </summary>
        [JsonPropertyName("presence_penalty")]
        public double PresencePenalty { get; set; } = 0;

        /// <summary>
        /// Number between -2.0 and 2.0. <br/>
        /// Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim. <br/>
        /// <a href="https://platform.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</a> <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-frequency_penalty" />
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public double FrequencyPenalty { get; set; } = 0;

       

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion. <br/>
        /// Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias value from -100 to 100. <br/>
        /// You can use this <a href="https://beta.openai.com/tokenizer?view=bpe"> tokenizer tool </a> (which works for both GPT-2 and GPT-3) to convert text to token IDs. <br/>
        /// Mathematically, the bias is added to the logits generated by the model prior to sampling. <br/>
        /// The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the relevant token. <br/>
        /// As an example, you can pass  <br/>
        /// to prevent the  <![CDATA[<|endoftext|>]]>  token from being generated. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-logit_bias" />
        /// </summary>
        [JsonPropertyName("logit_bias")]
        public Dictionary<string, int>? LogitBias { get; set; } = null;

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <a href="https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</a>.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// An object specifying the format that the model must output. <br/>
        /// Setting to { "type": "json_object" }
        /// enables JSON mode, which guarantees the message the model generates is valid JSON. <br/>
        /// Important: when using JSON mode, you must also instruct the model to produce JSON yourself via a system or user message.Without this, the model may generate an unending stream of whitespace until the generation reaches the token limit, resulting in a long-running and seemingly "stuck" request.Also note that the message content may be partially cut off if finish_reason= "length", which indicates the generation exceeded max_tokens or the conversation exceeded the max context length. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/completions/create#completions/create-logit_bias" />
        /// </summary>
        [JsonPropertyName("response_format")]
        public object? ResponseFormat { get; set; }
}
}

namespace OpenAI.Net
{
    public class Message
    {
        private static readonly string[] _validRoles = new string[] { ChatRoleType.User, ChatRoleType.System, ChatRoleType.Assistant };
        private Message(string role, string content)
        {
            Role = role;
            Content = content;
        }
        public string Role { get; init; }
        public string Content { get; init; }
        public static Message Create(string role, string content)
        {
            if (!_validRoles.Contains(role))
            {
                throw new ArgumentException($"Role must be one of the following ${string.Join(",", _validRoles)}", nameof(role));
            }
            return new Message(role, content);
        }
    }

    public class ChatRoleType
    {
        public const string User = "user";
        public const string System = "system";
        public const string Assistant = "assistant";
    }
}