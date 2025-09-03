using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentServiceController : ControllerBase
    {
        private readonly HttpClient client;
        public PaymentServiceController()
        {
            client = new HttpClient();
        }

        
        [HttpGet("fetch-all")]
        public async Task<IActionResult> FetchAllPayments()
        {
            var response = await client.GetAsync("https://localhost:7253/api/Payment");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            return BadRequest("Error fetching payments");
        }

        [HttpGet("fetch/{id}")]
        public async Task<IActionResult> FetchPaymentById(int id)
        {
            var response = await client.GetAsync($"https://localhost:7253/api/Payment/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            return NotFound($"Payment with ID {id} not found.");
        }

       
        [HttpPost("add")]
        public async Task<IActionResult> AddPayment(object model)
        {
            string data = System.Text.Json.JsonSerializer.Serialize(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7253/api/Payment", content);
            if (response.IsSuccessStatusCode)
                return Ok("Payment added successfully");
            return BadRequest("Error creating payment");
        }

   
        [HttpPut("modify/{id}")]
        public async Task<IActionResult> ModifyPayment(int id, object model)
        {
            string data = System.Text.Json.JsonSerializer.Serialize(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7253/api/Payment/{id}", content);
            if (response.IsSuccessStatusCode)
                return Ok("Payment updated successfully");
            return BadRequest("Error updating payment");
        }

        
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemovePayment(int id)
        {
            var response = await client.DeleteAsync($"https://localhost:7253/api/Payment/{id}");
            if (response.IsSuccessStatusCode)
                return Ok("Payment deleted successfully");
            return BadRequest("Error deleting payment");
        }
    }
}
