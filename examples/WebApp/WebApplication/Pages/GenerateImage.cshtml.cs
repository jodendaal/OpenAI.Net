using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI.Net;

namespace WebApplication.Pages
{
    public class GenerateImageModel : PageModel
    {
        private readonly ILogger<GenerateImageModel> _logger;
        private readonly IOpenAIService _openAIService;

        public List<string> Results { get; set; } = new List<string>();
        public string? ErrorMessage { get; set; }
        public GenerateImageModel(ILogger<GenerateImageModel> logger,IOpenAIService openAIService)
        {
            _logger = logger;
            _openAIService = openAIService;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string SearchText { get; set; } = "a otter hugging a cat with a sunset background framed with a heart";
        [BindProperty]
        public int MaxResults { get; set; } = 1;
        public async Task OnPost()
        {
            var response = await _openAIService.Images.Generate(SearchText, o => {
                o.N = MaxResults;
            });
            if(response.IsSuccess)
            {
                Results = response.Result!.Data.Select(i=> i.Url).ToList();
            }
            else
            {
                ErrorMessage = response.ErrorMessage;
            }
        }
    }
}