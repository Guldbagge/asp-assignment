using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            //if (HttpContext.Request.Cookies.TryGetValue("Access Tooken", out var token))
            //{ }
            return View();
        }
    }
}
