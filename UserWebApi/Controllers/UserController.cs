using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UserWebApi.Model;

namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<UserView> userList = new List<UserView>();
            var response = _client.GetAsync("https://localhost:7253/api/User").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<UserView>>(data);
            }
            return Ok(userList);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:7253/api/User", content);
            string responseContent = await response.Content.ReadAsStringAsync(); 

            if (response.IsSuccessStatusCode)
            {
                var id = JsonConvert.DeserializeObject<int>(responseContent);
                return Ok(id);
            }

            return BadRequest($"Error creating user: {responseContent}");
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, UserView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"https://localhost:7253/api/User/{Id}", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var id = JsonConvert.DeserializeObject<int>(responseContent);
                return Ok(id);
            }
            return BadRequest("Error updating user");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _client.DeleteAsync($"https://localhost:7253/api/User/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return Ok("deleted");
            }
            return BadRequest("Error deleting user");
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _client.GetAsync($"https://localhost:7253/api/User/{Id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                UserView user = JsonConvert.DeserializeObject<UserView>(data);
                return Ok(user);
            }
            return NotFound("User not found");
        }

 }
} // End of UserController.cs
