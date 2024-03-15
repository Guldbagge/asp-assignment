using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Courses";
            return View();
        }
    }
}
