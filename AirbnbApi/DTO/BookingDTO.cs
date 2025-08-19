using System;
using System.ComponentModel.DataAnnotations;

namespace AirbnbApi.DTO
{
    public class BookingDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Property ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Property ID must be a positive number")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]

        public string Title { get; set; }
        public string Name { get; set; }

        public DateTime CheckOutDate { get; set; }
        [Required]
        public int NumberofPeople { get; set; }
       
    }
}
