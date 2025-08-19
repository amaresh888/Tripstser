using HotelWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HotelController : ControllerBase
    {
        private readonly HttpClient _client;

        public HotelController()
        {
            _client = new HttpClient();
        }
        [HttpGet]
      

        public IActionResult Index()
        {
            List<HotelView> hotelist = new List<HotelView>();
            var response = _client.GetAsync("https://localhost:7253/api/Hotel").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                hotelist = JsonConvert.DeserializeObject<List<HotelView>>(data);
            }
            return Ok(hotelist);
        }
        [HttpPost]

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(HotelView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7253/api/Hotel", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("created");
            }
            return BadRequest("Error creating hotel");
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
       
        public async Task<IActionResult> Update(int id, HotelView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7253/api/Hotel/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("updated");
            }
            return BadRequest("Error updating hotel");
        }
        //[HttpDelete("{id}")]
        ////[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _client.DeleteAsync($"https://localhost:7253/api/Hotel/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return Ok("deleted");
        //    }
        //    return BadRequest("Error deleting hotel");

        //}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7253/api/Hotel/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var hotel = JsonConvert.DeserializeObject<HotelView>(data);
                return Ok(hotel);
            }
            return NotFound("Hotel not found");
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetByTitle(string Title)
        //{
        //    var response = await _client.GetAsync($"https://localhost:7253/api/Hotel/{Title}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        var hotel = JsonConvert.DeserializeObject<HotelView>(data);
        //        return Ok(hotel);
        //    }
        //    return NotFound("Hotel not found");

        //}

    }
}
