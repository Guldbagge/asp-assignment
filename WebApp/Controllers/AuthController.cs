using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AuthController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        public IActionResult Index()
        {
            ViewData["Title"] = "Profile";
            return View();
        }

        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
           var viewModel = new SignUpViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [Route("/signup")]
       
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(viewModel.Form);
                if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
                    return RedirectToAction("SignIn", "Auth");
            }
                return View(viewModel);
        }

        [Route("/signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }

        public new IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
                 return View(viewModel);

            // var result = _authService.SignIn(viewModel.Form);
            // if (result)
            //     return RedirectToAction("Index", "Account");


            viewModel.ErrorMessage = "Invalid email or password";
                return View(viewModel); 
            
        }

    }
}
