using System.ComponentModel.DataAnnotations;

namespace HRPortal.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public string LinkedIn { get; set; }

        [Required]
        public string WhyInterested { get; set; }
    }
}