 [![.NET](https://github.com/jodendaal/OpenAI.Net/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/jodendaal/OpenAI.Net/actions/workflows/dotnet-desktop.yml) [![NuGet Badge](https://buildstats.info/nuget/OpenAI.Net.Client)](https://www.nuget.org/packages/OpenAI.Net.Client)  ![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com//jodendaal/1823aaf39c6273b92442849479616daf/raw/OpenAI.Net-code-coverage.json)  [![Stryker Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Fjodendaal%2FOpenAI.Net%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/jodendaal/OpenAI.Net/main)  [![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/jodendaal/OpenAI.Net/blob/main/LICENSE)

# OpenAI.Net
OpenAI library for .NET

C# .NET library for use with the OpenAI API. 

This is community-maintained library.


# Getting started

Install package [Nuget package](https://www.nuget.org/packages/TimSoft.OpenAI.Net/)

```powershell
Install-Package OpenAI.Net.Client
```

Register services using the extension method

```csharp
 services.AddOpenAIServices(apiKey);
 OR
 services.AddOpenAIServices(apiKey, organizationId);
 OR
 services.AddOpenAIServices(apiKey, organizationId, apiUrl);
```
N.B We recommened using environment variables, configuration files or secret file for storing the API key securely. See [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows) for further details.

Inject the service where you need it.

e.g

```csharp
    public class MyAwsomeService 
    {
        private readonly IOpenAIService _openAIService;
        private readonly ILogger<MyAwsomeService> _logger;

        public MyAwsomeService(IOpenAIService openAIService,ILogger<MyAwsomeService> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
        }

        public async Task<TextCompletionResponse> Search()
        {
            var request = new TextCompletionRequest("text-davinci-003", "Say this is a test");
            var response = await _openAIService.TextCompletion.Get(request);
            if (response.IsSuccess)
            {
                return response.Result;
            }
            else
            {
                _logger.LogError(response.Exception, response.ErrorMessage, response.ErrorResponse);
            }

            return new TextCompletionResponse();
        }
    }


```

### Full support of all current API's
-   [x] [Models](https://beta.openai.com/docs/api-reference/models)
    -   [x] [List Models](https://beta.openai.com/docs/api-reference/models/list)
    -   [x] [Retrieve model](https://beta.openai.com/docs/api-reference/models/retrieve)
-   [x] [Completions](https://beta.openai.com/docs/api-reference/completions)
    -   [x] [Create completion](https://beta.openai.com/docs/api-reference/completions/create) 
    - [x] [Create completion with streaming](https://beta.openai.com/docs/api-reference/completions#completions/create-stream) 
-   [x] [Edits](https://beta.openai.com/docs/api-reference/edits) 
    -   [x] [Create edit](https://beta.openai.com/docs/api-reference/edits/create)
-   [x] [Images](https://beta.openai.com/docs/api-reference/images)
    -   [x] [Create image](https://beta.openai.com/docs/api-reference/images/create) 
    -   [x] [Create image edit](https://beta.openai.com/docs/api-reference/images/)
    -   [x] [Create image variation](https://beta.openai.com/docs/api-reference/images/create-variation)
-   [x] [Embeddings](https://beta.openai.com/docs/api-reference/embeddings)
    -   [x] [Create embeddings](https://beta.openai.com/docs/api-reference/embeddings/create)
-   [x] [Files](https://beta.openai.com/docs/api-reference/files)
    -   [x] [List files](https://beta.openai.com/docs/api-reference/files/list) 
    -   [x] [Upload file](https://beta.openai.com/docs/api-reference/files/upload) 
    -   [x] [Delete file](https://beta.openai.com/docs/api-reference/files/delete) 
    -   [x] [Retrieve file](https://beta.openai.com/docs/api-reference/files/retrieve) 
    -   [x] [Retrieve file content](https://beta.openai.com/docs/api-reference/files/retrieve-content) 

-   [x] [Fine-tunes](https://beta.openai.com/docs/api-reference/fine-tunes)
    -   [x] [Create fine-tune](https://beta.openai.com/docs/api-reference/fine-tunes)
    -   [x] [List fine-tunes](https://beta.openai.com/docs/api-reference/fine-tunes/list)
    -   [x] [Retrieve fine-tune](https://beta.openai.com/docs/api-reference/fine-tunes/retrieve)
    -   [x] [Cancel fine-tune](https://beta.openai.com/docs/api-reference/fine-tunes/cancel)
    -   [x] [List fine-tune events](https://beta.openai.com/docs/api-reference/fine-tunes/events)
    -   [x] [Delete fine-tune model](https://beta.openai.com/docs/api-reference/fine-tunes/delete-model)
-   [x] [Moderations](https://beta.openai.com/docs/api-reference/moderations)
    -   [x] [Create moderation](https://beta.openai.com/docs/api-reference/moderations/create)

# Testing
This project has 100% code coverage with Unit tests and 100% pass rate with [Stryker mutation testing](https://stryker-mutator.io/docs/stryker-net/introduction/). 

See latest Stryker report [here](https://dashboard.stryker-mutator.io/reports/github.com/jodendaal/OpenAI.Net/main#mutant).

We also have Integration tests foreach service.

This should provide confidence in the library going forwards.

# Contributions

Contributions are welcome.

Minimum requirements for any PR's.

- MUST include Unit tests and maintain 100% coverage.

- MUST pass Stryker mutation testing with 100%
- SHOULD have integration tests

# Examples

### Completion

```csharp
var response = await OpenAIService.TextCompletion.Get(model, "Say this is a test",(o) => {
                o.MaxTokens = 1024;
                o.BestOf = 2;
            });
```

### Completion Stream
```csharp
await foreach(var response in OpenAIService.TextCompletion.GetStream(request))
{
    Console.WriteLine(response?.Result?.Choices[0].Text);
}
```


### Text Edit
```csharp
var response = await service.Get("text-davinci-edit-001", "Fix the spelling mistakes", "What day of the wek is it?", (o =>{
    o.TopP = 0.1;
    o.Temperature = 100;
}));
```

### Image Edit
##### Using file paths
```csharp
var response = await service.Edit("A cute baby sea otter", @"Images\BabyCat.png", @"Images\Mask.png", o => {
    o.N = 99;
});
```

##### Using file bytes

```csharp
var response = await service.Edit("A cute baby sea otter",File.ReadAllBytes(@"Images\BabyCat.png"), File.ReadAllBytes(@"Images\BabyCat.png"), o => {
    o.N = 99;
});
```            

### Image Generate
```csharp
var response = await service.Generate("A cute baby sea otter",2, "1024x1024");
```