using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using Tripster_2.Models;

namespace Tripster_2.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client;
        private readonly UserController _db;
      
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
        public async Task<IActionResult> CreateUser(int id,UserViewModel user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7012/api/User", content);
            string responseContent = await response.Content.ReadAsStringAsync();

          
            if (response.IsSuccessStatusCode)
            {
                var userId = JsonConvert.DeserializeObject<int>(responseContent);
                return RedirectToAction("GetId", new {id = userId});

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
                return RedirectToAction("EditUser", new {id=UserId});
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
                return RedirectToAction("AllUsers");
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
        public async Task<IActionResult> GetId(int id)
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
