using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tripster_1.Models;

namespace Tripster_1.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            var response = await _client.GetAsync("https://localhost:7012/api/User");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                userList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(userList);
        }
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7012/api/User", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"API Error: {errorResponse}");
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7012/api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserViewModel>(data);
                return View(user);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");

            }
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int id,UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7012/api/User/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _client.GetAsync($"https://localhost:7012/api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserViewModel>(data);
                return View(user);
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");
            }
        }

    }
}
