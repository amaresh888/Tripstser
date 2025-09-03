using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;
using Tripster_2.Models;
  
namespace Tripster_2.Controllers
{
    public class BookingController : Controller
    {

        private readonly HttpClient _client;
        public BookingController()
        {
            _client = new HttpClient();
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> booklist = new List<BookViewModel>();
            var response = await _client.GetAsync("https://localhost:7228/api/Book");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                booklist = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }
            return View(booklist);
        }
        [HttpGet]
        public IActionResult CreateBooking(int PropertyId,string Title)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new BookViewModel
            {
                PropertyId = PropertyId,
                
                Title = Title,
                     
                UserId = (int)HttpContext.Session.GetInt32("UserId"),

                Name = HttpContext.Session.GetString("UserName")


            };
            return View(model);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookViewModel model)
         {
            model.UserId = (int)HttpContext.Session.GetInt32("UserId");
            
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7228/api/Book", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyBookings");


            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7228/api/Book/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookViewModel>(data);
                return View(book);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");

            }
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7228/api/Book/{id}", content);
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
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7228/api/Book/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7228/api/Book/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<BookViewModel>(data);
                return View(book);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");
            }


        }
        //[HttpGet]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var response = await _client.GetAsync($"https://localhost:7228/api/Book/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        var book = JsonConvert.DeserializeObject<BookViewModel>(data);

        //        if (book == null)
        //        {
        //            ViewBag.Error = "No booking found for this ID.";
        //            return View("Error"); // optional error view
        //        }

        //        return View(book); // ✅ Sends to Razor View
        //    }

        //    ViewBag.Error = $"API Error: {response.StatusCode}";
        //    return View("Error"); // safer than redirecting
        //}
        [HttpGet("MyBookings")]
        public async Task<IActionResult> MyBookings()
        {
            // get logged in user id from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            List<BookViewModel> myBookings = new List<BookViewModel>();
            var response = await _client.GetAsync($"https://localhost:7228/api/Book/GetBookingsByUser/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                myBookings = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
            }

            return View(myBookings);
        }


    }
}
