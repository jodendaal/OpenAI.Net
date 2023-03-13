using OpenAI.Net.Models.Responses.Common;

namespace OpenAI.Net.Models.Responses;

public class ChatCompletionBaseResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public int Created { get; set; }
    public string Model { get; set; }
    public Usage Usage { get; set; }
}

public class ChatCompletionResponse : ChatCompletionBaseResponse
{
    public ChatChoice[] Choices { get; set; }
}

public class ChatStreamCompletionResponse : ChatCompletionBaseResponse
{
    public ChatStreamChoice[] Choices { get; set; }
}

public class ChatChoice
{
    public Message Message { get; set; }
    public string Finish_reason { get; set; }
    public int Index { get; set; }
}

public class Message
{
    public string Role { get; set; }
    public string Content { get; set; }
}


public class ChatStreamChoice
{
    public Delta Delta { get; set; }
    public int Index { get; set; }
    public object Finish_reason { get; set; }
}

public class Delta
{
    public string Content { get; set; }
}

