using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirbnbApi.Models
{
    public class User
    {
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email can't be longer than 150 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Enter the Vaild PhoneNumber")]

        public double PhoneNumber { get; set; }


        public ICollection<Booking> Bookings { get; set; }
    }
}
