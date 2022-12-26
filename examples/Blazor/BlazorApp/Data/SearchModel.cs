using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class SearchModel
    {
        [Required]
        public string SearchText { get; set; }

        [Required]
        public int NoOfResults { get; set; } = 2;

        [Required]
        public int MaxTokens { get; set; } = 200;
    }
}