using System.ComponentModel.DataAnnotations;

namespace AuthServices.Models
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; }
    }
}
