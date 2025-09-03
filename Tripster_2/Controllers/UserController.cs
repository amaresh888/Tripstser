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

        async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            UserViewModel user = null;
            // This method should be implemented to retrieve the user by email from your database
            // For example, using UserManager or a custom repository
            using (var httpClient = new HttpClient())
            {
                //   httpClient.BaseAddress = new Uri("https://localhost:7253/");
                var response = await httpClient.GetAsync($"https://localhost:7253/api/User/UserByEmail/{email}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(data);
                    return user;
                }
            }
            return user;
        }
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(int id, UserViewModel user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7012/api/User", content);
            string responseContent = await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                var userId = JsonConvert.DeserializeObject<int>(responseContent);
                var user1 = await GetUserByEmailAsync(user.Email);
                if (user1 != null)
                {
                    HttpContext.Session.SetInt32("UserId", user1.UserId);
                    HttpContext.Session.SetString("UserName", user1.Name);
                }
                else
                {
                    //  _logger.LogWarning("User not found for email: {Email}", user.Email);
                }
                return RedirectToAction("GetId", "User", new { id = user1.UserId });


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
                return RedirectToAction("EditUser");
            }
            else
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, UserViewModel user)
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
        [HttpGet]
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


        
    

