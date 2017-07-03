using FlaveSite.Core.Projects;
using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjectService _projectService;

        public AdminController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult AddProject(AddProjectRequest request)
        {
            _projectService.AddProject(request);

            return new JsonResult("");
        }
    }
}