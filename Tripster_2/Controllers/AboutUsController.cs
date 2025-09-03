using Microsoft.AspNetCore.Mvc;

namespace Tripster_2.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
