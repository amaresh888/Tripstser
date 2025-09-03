using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string query)
        {
            string answer = "";

            if (query.Contains("hotels"))
                answer = "Currently we have 5 hotels available in Hyderabad.";
            else if (query.Contains("booking"))
                answer = "Your booking status can be checked in the MyBookings section.";
            else
                answer = "Sorry, I can only answer hotel related queries.";

            return Ok(answer);
        }
    }
}
