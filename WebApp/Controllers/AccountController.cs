using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };
        viewModel.BasicInfo ??= await PopulateAccountDetailsBasicInfoAsync();
        viewModel.AddressInfo ??= new AccountDetailsAddressInfoModel();
        
        return View(viewModel);
    }

    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (ModelState.IsValid)
        { 
            if (viewModel.BasicInfo!=null) { }
            if (viewModel.AddressInfo!=null) { }
        }
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfo ??= await PopulateAccountDetailsBasicInfoAsync();
        viewModel.AddressInfo ??= new AccountDetailsAddressInfoModel();
        return View(viewModel);

    }

    //[HttpPost]
    //public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
    //{
    //    if (TryValidateModel(viewModel.AddressInfo))
    //    {
    //        return RedirectToAction("Home", "Default");
    //    }


    //    return View("Details", viewModel);
    //}


    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel()
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
        };
    }

    private async Task<AccountDetailsBasicInfoModel> PopulateAccountDetailsBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        return new AccountDetailsBasicInfoModel()
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Biography = user.Bio,
        };

    }

    ////private readonly AccountService _accountService;

    ////public AccountController(AccountService accountService)
    ////{
    ////    _accountService = accountService;
    ////}

    //[Route("/account")]
    //public IActionResult Details()
    //{
    //    var viewModel = new AccoundDetailsViewModel();
    //    //viewModel.BasicInfo = _accountService.GetBasicInfo();
    //    //viewModel.AddressInfo = _accountService.GetAddressInfo();
    //    return View(viewModel);
    //}

    //[HttpPost]
    //public IActionResult BasicInfo(AccoundDetailsViewModel viewModel)
    //{
    //    //_accountService.SaveBasicInfo(viewModel.BasicInfo);
    //    return RedirectToAction(nameof(Details));
    //}

    //[HttpPost]
    //public IActionResult AddressInfo(AccoundDetailsViewModel viewModel)
    //{
    //    //_accountService.SaveAdressInfo(viewModel.AdressInfo);
    //    return RedirectToAction(nameof(Details));
    //}
}
