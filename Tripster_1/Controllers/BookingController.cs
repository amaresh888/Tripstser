using Microsoft.AspNetCore.Mvc;

namespace Tripster_1.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
