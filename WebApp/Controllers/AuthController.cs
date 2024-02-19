using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Profile";
            return View();
        }

        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign In";
            return View();
        }
        [Route("/SignUp")]
        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign Up";
            return View();
        }

        public new IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

     }
}
