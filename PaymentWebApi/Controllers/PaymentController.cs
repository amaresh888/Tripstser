using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentWebApi.Models;

namespace PaymentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _Client;
        public PaymentController()
        {
            _Client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllPayments()
        {
            List<PaymentView> payments = new List<PaymentView>();
            var response = await _Client.GetAsync("https://localhost:7253/api/Payment");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                payments = JsonConvert.DeserializeObject<List<PaymentView>>(data);
                return Ok(payments);
            }
            return BadRequest("No payments found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FetchPaymentById(int id)
        {
            var response = await _Client.GetAsync($"https://localhost:7253/api/Payment/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var payment = JsonConvert.DeserializeObject<PaymentView>(data);
                return Ok(payment);
            }
            return NotFound($"Payment with ID {id} not found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPayment(PaymentView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await _Client.PostAsync("https://localhost:7253/api/Payment", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Payment Created Successfully");
            }
            return BadRequest("Error creating payment");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPayment(int id, PaymentView model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await _Client.PutAsync($"https://localhost:7253/api/Payment/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Payment Updated Successfully");
            }
            return BadRequest("Error updating payment");
        }
    }
}
