using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
