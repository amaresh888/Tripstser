using AirbnbApi.Data;
using AirbnbApi.DTO;
using AirbnbApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public HotelController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _db.hotels.ToList();
            if (obj == null)
            {
                return NotFound("No hotels found.");
            }
            return Ok(obj);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var obj = _db.hotels.Find(id);
            if (obj == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult Create([FromBody] HotelDTO hotelDto)
        {
            if (hotelDto == null)
            {
                return BadRequest("Hotel data is null.");
            }

            var hotel = new Hotel
            {
                Title = hotelDto.Title,
                Location = hotelDto.Location,
                PricePerNight = hotelDto.PricePerNight,
                Description = hotelDto.Description
            };

            _db.hotels.Add(hotel);
            _db.SaveChanges();

            hotelDto.PropertyId = hotel.PropertyId; 
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] HotelDTO dto)
        {
            var hotel = _db.hotels.Find(dto.PropertyId);
            if (hotel is null)
                return NotFound($"Hotel with ID {dto.PropertyId} not found.");

            
            hotel.Title = dto.Title;
            hotel.Location = dto.Location;
            hotel.Description = dto.Description;
            hotel.PricePerNight = dto.PricePerNight;
            _db.hotels.Update(hotel);

            _db.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var obj = _db.hotels.Find(id);
            if (obj == null)
            {
                return NotFound($"Hotel with ID {id} not found.");
            }
            _db.hotels.Remove(obj);
            _db.SaveChanges();
            return Ok($"Hotel with ID {id} deleted successfully.");



        }
    }
}

