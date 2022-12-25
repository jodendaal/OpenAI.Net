﻿using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses.Common;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Services.Interfaces;

namespace OpenAI.Net.Extensions
{
    public static class TextCompletionServiceExtentionMethods
    {
        public static Task<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> Get(this ITextCompletionService textCompletion, string model, string prompt, Action<TextCompletionRequest>? options = null)
        {
            var request = new TextCompletionRequest(model, prompt);
            options?.Invoke(request);
            return textCompletion.Get(request);
        }

        public static IAsyncEnumerable<OpenAIHttpOperationResult<TextCompletionResponse, ErrorResponse>> GetStream(this ITextCompletionService textCompletion, string model, string prompt, Action<TextCompletionRequest>? options = null)
        {
            var request = new TextCompletionRequest(model, prompt);
            options?.Invoke(request);
            return textCompletion.GetStream(request);
        }
    }
}
