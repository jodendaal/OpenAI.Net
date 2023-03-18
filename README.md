 [![.NET](https://github.com/jodendaal/OpenAI.Net/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/jodendaal/OpenAI.Net/actions/workflows/dotnet-desktop.yml) [![NuGet Badge](https://buildstats.info/nuget/OpenAI.Net.Client)](https://www.nuget.org/packages/OpenAI.Net.Client)  ![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com//jodendaal/1823aaf39c6273b92442849479616daf/raw/OpenAI.Net-code-coverage.json)  [![Stryker Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Fjodendaal%2FOpenAI.Net%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/jodendaal/OpenAI.Net/main)  [![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/jodendaal/OpenAI.Net/blob/main/LICENSE) [![Visitors](https://api.visitorbadge.io/api/visitors?path=https%3A%2F%2Fgithub.com%2Fjodendaal%2FOpenAI.Net&countColor=%2344be16&style=flat&labelStyle=lower)](https://visitorbadge.io/status?path=https%3A%2F%2Fgithub.com%2Fjodendaal%2FOpenAI.Net)

 
<img src="https://raw.githubusercontent.com/jodendaal/OpenAI.Net/main/src/OpenAI.Net/OpenAILogo.svg" alt="OpenAI" width="300"/>


# OpenAI.Net
OpenAI library for .NET

C# .NET library for use with the OpenAI API. 

This is community-maintained library.

This library supports .net core 6.0 and above.

Youtube tutorial : [Creating your own ChatGPT clone with .NET: A Step-by-Step Guide](https://www.youtube.com/watch?v=hRkVGSMijjs&t=51s)

# Getting started

Install package [Nuget package](https://www.nuget.org/packages/OpenAI.Net.Client)

```powershell
Install-Package OpenAI.Net.Client
```

Register services using the provided extension methods

```csharp
services.AddOpenAIServices(options => {
    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
});
```

N.B We recommend using environment variables, configuration files or secret file for storing the API key securely. See [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows) for further details.


# Example Usage

You can view examples of a console, web application and Blazor app using the streaming API [here](https://github.com/jodendaal/OpenAI.Net/tree/main/examples). 

You can also have a look at the Integration Tests for usage examples [here](https://github.com/jodendaal/OpenAI.Net/tree/main/src/OpenAI.Net.Integration.Tests).

Simple console app usage below.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.Net;

namespace ConsoleApp
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddOpenAIServices(options => {
                    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
                });
            })
            .Build();

            var openAi = host.Services.GetService<IOpenAIService>();
            var response = await openAi.TextCompletion.Get("How long until we reach mars?");

            if (response.IsSuccess)
            {
                foreach(var result in response.Result.Choices)
                {
                    Console.WriteLine(result.Text);
                }
            }
            else
            {
                Console.WriteLine($"{response.ErrorMessage}");
            }
        }
    }
}
```

# Configuring Http Client Options
 The registration extension allows for configuration of the http client via the IHttpClientBuilder interface.
 This allows for adding a Polly retry policy for example. See example [here](https://github.com/jodendaal/OpenAI.Net/tree/main/examples/Console/ConsoleAppWithPolly).

```csharp
services.AddOpenAIServices(options => {
    options.ApiKey = builder.Configuration["OpenAI:ApiKey"];
}, 
(httpClientOptions) => {
    httpClientOptions.AddPolicyHandler(GetRetryPolicy());
});

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                    retryAttempt)));
}
```






# Examples

### Completion

```csharp
var response = await service.TextCompletion.Get("Say this is a test",(o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
            });
```

### Completion Stream
```csharp
await foreach(var response in service.TextCompletion.GetStream("Say this is a test"))
{
    Console.WriteLine(response?.Result?.Choices[0].Text);
}
```
### Completion with array input
```csharp

var prompts = new List<string>()
{
    "Say this is a test",
    "Say this is not a test"
};

var response = await service.TextCompletion.Get(prompts,(o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
            });
```

### Chat

```csharp
var messages = new List<Message>
{
    Message.Create(ChatRoleType.System, "You are a helpful assistant."),
    Message.Create(ChatRoleType.User, "Who won the world series in 2020?"),
    Message.Create(ChatRoleType.Assistant, "The Los Angeles Dodgers won the World Series in 2020."),
    Message.Create(ChatRoleType.User, "Where was it played?")
};

 var response = await service.Chat.Get(messages,o => {
    o.MaxTokens = 1000;
});
```

### Chat Stream
```csharp
var messages = new List<Message>
{
    Message.Create(ChatRoleType.System, "You are a helpful assistant."),
    Message.Create(ChatRoleType.User, "Who won the world series in 2020?"),
    Message.Create(ChatRoleType.Assistant, "The Los Angeles Dodgers won the World Series in 2020."),
    Message.Create(ChatRoleType.User, "Where was it played?")
};

await foreach (var t in service.Chat.GetStream(messages))
{
    Console.WriteLine(t?.Result?.Choices[0].Delta?.Content);
}
```

### Audio
##### Get transcription
```csharp
var response = await OpenAIService.Audio.GetTranscription(@"Audio\TestTranscription.m4a");
```

##### Get translation
```csharp
var response = await OpenAIService.Audio.GetTranslation(@"Audio\Translation.m4a");
```


### Text Edit
```csharp
var response = await service.TextEdit.Get("Fix the spelling mistakes", "What day of the wek is it?", (o =>{
    o.TopP = 0.1;
    o.Temperature = 100;
}));
```

### Image Edit
##### Using file paths
```csharp
var response = await service.Images.Edit("A cute baby sea otter", @"Images\BabyCat.png", @"Images\Mask.png", o => {
    o.N = 99;
});
```

##### Using file bytes

```csharp
var response = await service.Images.Edit("A cute baby sea otter",File.ReadAllBytes(@"Images\BabyCat.png"), File.ReadAllBytes(@"Images\BabyCat.png"), o => {
    o.N = 99;
});
```            

### Image Generate
```csharp
var response = await service.Images.Generate("A cute baby sea otter",2, "1024x1024");
```

### Image Variation
##### Using file paths
```csharp
var response = await service.Images.Variation(@"Images\BabyCat.png", o => {
    o.N = 2;
    o.Size = "1024x1024";
});
```
##### Using file bytes
```csharp
 var response = await service.Images.Variation(File.ReadAllBytes(@"Images\BabyCat.png"), o => {
                o.N = 2;
                o.Size = "1024x1024";
});
```

### Fine Tune Create
```csharp
var response = await service.FineTune.Create("myfile.jsonl", o => {
    o.BatchSize = 1;
});
```

### Fine Tune Get All
```csharp
var response = await service.FineTune.Get();
```

### Fine Tune Get By Id
```csharp
var response = await service.FineTune.Get("fineTuneId");
```

### Fine Tune Get Events
```csharp
var response = await service.FineTune.GetEvents("fineTuneId");
```

### Fine Tune Delete
```csharp
var response = await service.FineTune.Delete("modelId");
```

### Fine Tune Cancel
```csharp
var response = await service.FineTune.Cancel("fineTuneId");
```

### File Upload
##### Using file path
```csharp
var response = await service.Files.Upload(@"Images\BabyCat.png");
```
##### Using file bytes
```csharp
var response = await service.Files.Upload(bytes, "mymodel.jsonl");
```
### File Get Content
```csharp
 var response = await service.Files.GetContent("fileId");
```
### File Get File Detail
```csharp
var response = await service.Files.Get("fileId");
```
### File Get File All
```csharp
var response = await service.Files.Get();
```
### File Delete
```csharp
var response = await service.Files.Delete("1");
```
### Emdeddings Create
```csharp
var response = await service.Embeddings.Create("The food was delicious and the waiter...", "text-embedding-ada-002", "test");
```

### Models Get All
```csharp
var response = await service.Models.Get();
```

### Models Get By Id
```csharp
var response = await service.Models.Get("babbage");
```

### Moderation Create
```csharp
var response = await service.Moderation.Create("input text", "test");
```

# Testing
This project has 100% code coverage with Unit tests and 100% pass rate with [Stryker mutation testing](https://stryker-mutator.io/docs/stryker-net/introduction/). 

See latest Stryker report [here](https://dashboard.stryker-mutator.io/reports/github.com/jodendaal/OpenAI.Net/main#mutant).

We also have Integration tests foreach service.

This should provide confidence in the library going forwards.

# Supported API's
### Full support of all current API's
-   [x] [Chat](https://platform.openai.com/docs/api-reference/chat)
    -   [x] [Create Chat Completion](https://platform.openai.com/docs/api-reference/chat/create)
    
-   [x] [Audio](https://platform.openai.com/docs/api-reference/audio)
    -   [x] [Create transcription](https://platform.openai.com/docs/api-reference/audio/create)
    -   [x] [Create translation](https://platform.openai.com/docs/api-reference/audio/create)
        
-   [x] [Models](https://platform.openai.com/docs/api-reference/models)
    -   [x] [List Models](https://platform.openai.com/docs/api-reference/models/list)
    -   [x] [Retrieve model](https://platform.openai.com/docs/api-reference/models/retrieve)
-   [x] [Completions](https://platform.openai.com/docs/api-reference/completions)
    -   [x] [Create completion](https://platform.openai.com/docs/api-reference/completions/create) 
    - [x] [Create completion with streaming](https://platform.openai.com/docs/api-reference/completions#completions/create-stream) 
-   [x] [Edits](https://platform.openai.com/docs/api-reference/edits) 
    -   [x] [Create edit](https://platform.openai.com/docs/api-reference/edits/create)
-   [x] [Images](https://platform.openai.com/docs/api-reference/images)
    -   [x] [Create image](https://platform.openai.com/docs/api-reference/images/create) 
    -   [x] [Create image edit](https://platform.openai.com/docs/api-reference/images/)
    -   [x] [Create image variation](https://platform.openai.com/docs/api-reference/images/create-variation)
-   [x] [Embeddings](https://platform.openai.com/docs/api-reference/embeddings)
    -   [x] [Create embeddings](https://platform.openai.com/docs/api-reference/embeddings/create)
-   [x] [Files](https://platform.openai.com/docs/api-reference/files)
    -   [x] [List files](https://platform.openai.com/docs/api-reference/files/list) 
    -   [x] [Upload file](https://platform.openai.com/docs/api-reference/files/upload) 
    -   [x] [Delete file](https://platform.openai.com/docs/api-reference/files/delete) 
    -   [x] [Retrieve file](https://platform.openai.com/docs/api-reference/files/retrieve) 
    -   [x] [Retrieve file content](https://platform.openai.com/docs/api-reference/files/retrieve-content) 

-   [x] [Fine-tunes](https://platform.openai.com/docs/api-reference/fine-tunes)
    -   [x] [Create fine-tune](https://platform.openai.com/docs/api-reference/fine-tunes)
    -   [x] [List fine-tunes](https://platform.openai.com/docs/api-reference/fine-tunes/list)
    -   [x] [Retrieve fine-tune](https://platform.openai.com/docs/api-reference/fine-tunes/retrieve)
    -   [x] [Cancel fine-tune](https://platform.openai.com/docs/api-reference/fine-tunes/cancel)
    -   [x] [List fine-tune events](https://platform.openai.com/docs/api-reference/fine-tunes/events)
    -   [x] [Delete fine-tune model](https://platform.openai.com/docs/api-reference/fine-tunes/delete-model)
-   [x] [Moderations](https://platform.openai.com/docs/api-reference/moderations)
    -   [x] [Create moderation](https://platform.openai.com/docs/api-reference/moderations/create)

# Contributions

Contributions are welcome.

Minimum requirements for any PR's.

- MUST include Unit tests and maintain 100% coverage.

- MUST pass Stryker mutation testing with 100%
- SHOULD have integration tests

# Buy me a coffee 
https://www.buymeacoffee.com/timdoestech?new=1
