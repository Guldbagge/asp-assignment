using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

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
        [HttpGet]
        public IActionResult SignUp()
        {
           var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [Route("/SignUp")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
           
 
            return RedirectToAction("SignIn", "Auth");
        }

        public new IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

     }
}
