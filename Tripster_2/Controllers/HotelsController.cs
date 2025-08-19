using HotelWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tripster_2.Models; // Make sure this namespace contains HotelViewModel

namespace Tripster_2.Controllers
{
    public class HotelsController : Controller
    {
        private readonly HttpClient _client;

        public HotelsController()
        {
            _client = new HttpClient();
           
        }
        //[AllowAnonymous]
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
        [HttpGet]
     // [Authorize]
        public IActionResult Create()
        {
            // Check session for logged-in user
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(HotelViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7144/api/Hotel", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Error: {errorResponse}");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userName = HttpContext.Session.GetString("userName");
            if (string.IsNullOrEmpty(userName))
            {
                
                return Redirect("/Identity/Account/Login");
            }
            var response = await _client.GetAsync($"https://localhost:7144/api/Hotel/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var hotel = JsonConvert.DeserializeObject<HotelViewModel>(data);
                return View(hotel);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");

            }
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, HotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7144/api/Hotel/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return View(model);
            }
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7144/api/Hotel/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7144/api/Hotel/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var hotel = JsonConvert.DeserializeObject<HotelViewModel>(data);
                return View(hotel);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetbyTitle(int Title)
        //{
        //    var response = await _client.GetAsync($"https://localhost:7144/api/Hotel/{Title}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        var hotel = JsonConvert.DeserializeObject<HotelViewModel>(data);
        //        return View(hotel);
        //    }
        //    else
        //    {
        //        ViewBag.Error = $"API Error: {response.StatusCode}";
        //        return RedirectToAction("Index");
        //    }
        }
    }


