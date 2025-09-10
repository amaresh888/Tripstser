using Microsoft.AspNetCore.Mvc;

namespace Tripster_2.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
