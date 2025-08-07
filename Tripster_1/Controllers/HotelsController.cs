using HotelWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tripster_1.Models; // Make sure this namespace contains HotelViewModel

namespace Tripster_1.Controllers
{
    public class HotelsController : Controller
    {
        private readonly HttpClient _client;

        public HotelsController()
        {
            _client = new HttpClient();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<HotelViewModel> hotelList = new List<HotelViewModel>();
            var response = await _client.GetAsync("https://localhost:7144/api/Hotel");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                hotelList = JsonConvert.DeserializeObject<List<HotelViewModel>>(data);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
            }

            return View(hotelList);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Create(HotelView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7144/api/Hotel", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("created");
            }
            return BadRequest("Error creating hotel");
        }


    }
}
