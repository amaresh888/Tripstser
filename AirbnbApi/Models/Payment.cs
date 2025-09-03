using AirbnbApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelbnbApi.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }   

        [Required(ErrorMessage = "BookingId is required")]
        [ForeignKey("Booking")]            
        public int BookingId { get; set; }

     

        [Required]
      
        public int Amount { get; set; }  

        [Required(ErrorMessage = "Payment Method is required")]
        [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters")]
        public string PaymentMethod { get; set; } 
        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Pending";   

        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public Booking Booking { get; set; }
    }
}
