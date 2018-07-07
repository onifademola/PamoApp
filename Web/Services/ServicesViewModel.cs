using System.ComponentModel.DataAnnotations;

namespace Web.Services
{
    public class EmailMessage
    {
        [Required]
        public string Subject { get; set; }        
        public string Body { get; set; }
        [Required]
        [EmailAddress]
        public string Recipient { get; set; }
    }
}