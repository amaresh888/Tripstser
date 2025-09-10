
using BookWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookController : ControllerBase
    {
        private readonly HttpClient _client;

        public BookController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookView> booklist = new List<BookView>();
            var response = await _client.GetAsync("https://localhost:7253/api/Booking");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                booklist = JsonConvert.DeserializeObject<List<BookView>>(data);
            }

            return Ok(booklist);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(BookView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7253/api/Booking", content);

            if (response.IsSuccessStatusCode)
                return Ok("Created");
            
            return BadRequest("Error creating booking");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"https://localhost:7253/api/Booking/{id}", content);

            if (response.IsSuccessStatusCode)
                return Ok("Updated");

            return BadRequest("Error updating booking");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7253/api/Booking/{id}");

            if (response.IsSuccessStatusCode)
                return Ok("Deleted");

            return BadRequest("Error deleting booking");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7253/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookView>(data);
                return Ok(book);
            }
            return NotFound("Booking not found");
        }
        [HttpGet("GetBookingsByUser/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(int userId)
        {
            List<BookView> userBookings = new List<BookView>();

           
            var response = await _client.GetAsync($"https://localhost:7253/api/Booking/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                userBookings = JsonConvert.DeserializeObject<List<BookView>>(data);
                return Ok(userBookings);
            }

            return BadRequest("Unable to fetch bookings for this user");
        }


    }
}
