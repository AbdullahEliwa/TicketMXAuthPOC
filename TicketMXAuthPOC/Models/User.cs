using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TicketMXAuthPOC.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
