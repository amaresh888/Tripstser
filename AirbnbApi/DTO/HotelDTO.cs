using System.ComponentModel.DataAnnotations;

namespace AirbnbApi.DTO
{
    public class HotelDTO
    {
        public int PropertyId { get; set; } // Usually auto-generated, so no validation here

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title can't exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(150, ErrorMessage = "Location can't exceed 150 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Price per night is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price per night must be a positive number")]
        public int PricePerNight { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        public string ImageUrl { get; set; } 
    }
}
