using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("IndexOld");
        }

        public IActionResult Testing()
        {
            return View("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
