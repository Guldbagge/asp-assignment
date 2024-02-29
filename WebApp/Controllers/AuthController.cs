using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.ViewModels;
namespace WebApp.Controllers;

public class AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService) : Controller
{
    private readonly UserService _userService = userService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

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
            if (!await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email))
            {
                var applicationUser = new ApplicationUser
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                    UserName = viewModel.Form.Email
                };
                var result = await _userManager.CreateAsync(applicationUser, viewModel.Form.Password);

                if (result.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(applicationUser, viewModel.Form.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Auth");
                    }
                }
            }
        }
        return View(viewModel);
    }




    //public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _userService.CreateUserAsync(viewModel.Form);
    //        if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
    //            return RedirectToAction("SignIn", "Auth");
    //    }
    //    return View(viewModel);
    //}

    [HttpGet]
    [Route("/signin")]
  
    public IActionResult SignIn() => View(new SignInViewModel());




    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
    viewModel.Email,
    viewModel.Password,
    viewModel.RememberMe,
    false
);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }

        }
        ViewData["ErrorMessage"] = "Invalid Email or Password";
        return View(viewModel);
    }


    //[Route("/signin")]
    //[HttpPost]
    //public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _userService.SignInUserAsync(viewModel.Form);
    //        if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
    //            return RedirectToAction("Details", "Account");
    //    }

    //    viewModel.ErrorMessage = "Incorrect email or password";
    //    return View(viewModel);


    //}

}
