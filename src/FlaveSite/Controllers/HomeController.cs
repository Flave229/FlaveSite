using FlaveSite.Core.Projects;
using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectRepository _projectRepository;

        public HomeController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;    
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "This is what I get up to.";

            _projectRepository.GetProgrammingLanguages();

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
