using FlaveSite.Core.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace FlaveSite.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Mostly Coherent Ramblings";

            var blogItems = _blogService.GetBlogs();

            return View("Index", blogItems);
        }

        public IActionResult Test()
        {
            return View("TestBlog");
        }
    }
}
