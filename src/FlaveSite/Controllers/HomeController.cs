using FlaveSite.Core.Projects;
using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectService _projectService;

        public HomeController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "This is what I get up to.";

            var projects = _projectService.GetProjects();

            return View("Index", projects);
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

        public IActionResult ProjectDetails(int projectId)
        {
            var project = _projectService.GetProjectDetails(projectId);
            return View(project);
        }
    }
}
