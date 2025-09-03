using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirbnbApi.Models
{
    public class Hotel
    {
        [Key]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(150, ErrorMessage = "Location can't be longer than 150 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Price per night is required")]
        [Range(1, 100000, ErrorMessage = "Price per night must be between 1 and 100000")]
        public int PricePerNight { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters")]
        public string Description { get; set; }

        public string ImageUrl { get; set; } 

        // Navigation property
        public ICollection<Booking> Bookings { get; set; }
    }
}
  