using System.ComponentModel.DataAnnotations;

namespace OpenAI.Net.Models.Requests
{
    public class FileUploadRequest
    {
        public FileUploadRequest(FileContentInfo file,string purpose = "fine-tune") 
        {
            File = file;
            Purpose = purpose;
        }

        [Required]
        public FileContentInfo File { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
