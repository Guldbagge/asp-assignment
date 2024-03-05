using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{


    [HttpGet]
    [Route("/account/details")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SaveBasicInfo(AccountDetailsViewModel viewModel)
    {
        if (TryValidateModel(viewModel.BasicInfo))
        {
            return RedirectToAction("Home", "Default");
        }


        return View("Details", viewModel);
    }

    [HttpPost]
    public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
    {
        if (TryValidateModel(viewModel.AddressInfo))
        {
            return RedirectToAction("Home", "Default");
        }


        return View("Details", viewModel);
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
