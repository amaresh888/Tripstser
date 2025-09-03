using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using HotelbnbApi.DTO;
using HotelbnbApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

    
        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _db.Payments.ToList();
            if(payments==null)
                                return NotFound("No payments found.");
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound($"Payment with ID {id} not found.");
            return Ok(payment);
        }

    
        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDTO dto)
        {
            
            

            var payment = new Payment
            {
                 BookingId = dto.BookingId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentStatus ?? "Pending",
                PaymentDate = DateTime.Now
            };

            _db.Payments.Add(payment);
            _db.SaveChanges();

            return Ok(payment);
        }

      
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentDTO dto)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound($"Payment with ID {id} not found.");

            payment.Amount = dto.Amount;
            payment.PaymentMethod = dto.PaymentMethod;
            payment.PaymentStatus = dto.PaymentStatus;
            _db.Payments.Update(payment);
            _db.SaveChanges();

            return Ok(payment);
        }

        // DELETE: api/payment/5
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound($"Payment with ID {id} not found.");

            _db.Payments.Remove(payment);
            _db.SaveChanges();
            return Ok($"Payment with ID {id} deleted successfully.");
        }
    }
}
