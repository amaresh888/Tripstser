using System.ComponentModel.DataAnnotations;

namespace Tripster_2.Models
{
    public class BookViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
        public int UserId { get; set; }
        public string Name { get; set; }
        //public int PhoneNumber { get; set; }
        //public string Email { get; set; }

        [Required(ErrorMessage = "Property ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Property ID must be a positive number")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]

        public DateTime CheckOutDate { get; set; } = DateTime.Now;
        public int NumberofPeople { get; set; } = 1;
        public string Title { get; set; }
    }
}
