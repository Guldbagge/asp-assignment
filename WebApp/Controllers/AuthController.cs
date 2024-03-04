using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, UserService userService) : Controller
{
    private readonly UserService _userService = userService;
    private readonly UserManager<UserEntity> _userManager = userManager;


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
            var exist = await _userManager.FindByEmailAsync(viewModel.Form.Email);
            if (exist != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                ViewData["ErrorMessage"] = "User with the same email already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }
        }

        return View(viewModel);
    }


    [HttpGet]
    [Route("/signin")]
  
    public IActionResult SignIn() => View(new SignInViewModel());
 



    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.SignInUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
                return RedirectToAction("Details", "Account");
        }
        
        viewModel.ErrorMessage = "Incorrect email or password";
        return View(viewModel);


    }

}


//using Infrastructure.Services;
//using Microsoft.AspNetCore.Mvc;
//using WebApp.ViewModels;

//namespace WebApp.Controllers;

//public class AuthController(UserService userService) : Controller
//{
//    private readonly UserService _userService = userService;

//    public IActionResult Index()
//    {
//        ViewData["Title"] = "Profile";
//        return View();
//    }

//    [Route("/signup")]
//    [HttpGet]
//    public IActionResult SignUp()
//    {
//        var viewModel = new SignUpViewModel();
//        return View(viewModel);
//    }

//    [HttpPost]
//    [Route("/signup")]

//    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
//    {
//        if (ModelState.IsValid)
//        {
//            var result = await _userService.CreateUserAsync(viewModel.Form);
//            if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
//                return RedirectToAction("SignIn", "Auth");
//        }
//        return View(viewModel);
//    }

//    [HttpGet]
//    [Route("/signin")]

//    public IActionResult SignIn() => View(new SignInViewModel());




//    [Route("/signin")]
//    [HttpPost]
//    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
//    {
//        if (ModelState.IsValid)
//        {
//            var result = await _userService.SignInUserAsync(viewModel.Form);
//            if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
//                return RedirectToAction("Details", "Account");
//        }

//        viewModel.ErrorMessage = "Incorrect email or password";
//        return View(viewModel);


//    }

//}
