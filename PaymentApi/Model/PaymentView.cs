using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApi.Model
{
    public class PaymentView
    {
      
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
