using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripster_2.Models;

namespace Tripster_2.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly HttpClient _client;

        public PaymentsController()
        {
            _client = new HttpClient();
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            List<PaymentViewModel> PaymentList = new List<PaymentViewModel>();
            var response = await _client.GetAsync("https://localhost:7233/api/Payment");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                PaymentList = JsonConvert.DeserializeObject<List<PaymentViewModel>>(data);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
            }

            return View(PaymentList);
        }
        [HttpGet]
        // [Authorize]
        public IActionResult Create()
        {
            // Check session for logged-in user

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7233/api/Payment", content);

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
            var response = await _client.GetAsync($"https://localhost:7233/api/Payment/{id}");
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
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7233/api/Payment /{id}", content);
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
    }
}
