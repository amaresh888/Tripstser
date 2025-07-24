using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
             var bookings = _db.bookings.ToList();
            if (bookings == null || !bookings.Any())
            {
                return NotFound("No bookings found.");
            }
            return Ok(bookings);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = _db.bookings.Find(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }
            return Ok(booking);
        }
        [HttpPost]
        public IActionResult Create([FromBody] BookingDTO book)
        {
            if (book == null)
            {
                return BadRequest("Booking data is null.");
            }
            var booking = new Booking
            {
                UserId = book.UserId,
                PropertyId = book.PropertyId,
                CheckInDate = book.CheckInDate,
                CheckOutDate = book.CheckOutDate
            };

            _db.bookings.Add(booking);
            _db.SaveChanges();

            book.Id = booking.Id; 
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BookingDTO book)
        {
            if (book == null)
            {
                return BadRequest("Booking data is null.");
            }
            var existingBooking = _db.bookings.Find(id);
            if (existingBooking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }
            existingBooking.UserId = book.UserId;
            existingBooking.PropertyId = book.PropertyId;
            existingBooking.CheckInDate = book.CheckInDate;
            existingBooking.CheckOutDate = book.CheckOutDate;
            
            _db.SaveChanges();
            return Ok(existingBooking);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _db.bookings.Find(id);
            if (booking == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }
            _db.bookings.Remove(booking);
            _db.SaveChanges();
            return Ok($"Booking with ID {id} deleted successfully.");
      
        }}
}
