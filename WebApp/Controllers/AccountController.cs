//using Infrastructure.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApp.Models;
//using WebApp.ViewModels;

//namespace WebApp.Controllers
//{
//    [Authorize]
//    public class AccountController : Controller
//    {
//        private readonly AccountService _accountService;

//        public AccountController(AccountService accountService)
//        {
//            _accountService = accountService;
//        }

//        [Route("/account")]
//        public IActionResult Details()
//        {
//            var viewModel = new AccoundDetailsViewModel();
//            viewModel.BasicInfo = _accountService.GetBasicInfo();
//            viewModel.AddressInfo = _accountService.GetAddressInfo();
//            return View(viewModel);
//        }

//        [HttpPost]
//        public IActionResult BasicInfo(AccountDetailsBasicInfoModel basicInfo)
//        {
//            _accountService.SaveBasicInfo(basicInfo);
//            return RedirectToAction(nameof(Details));
//        }

//        [HttpPost]
//        public IActionResult AddressInfo(AccountDetailsAddressInfoModel addressInfo)
//        {
//            _accountService.SaveAddressInfo(addressInfo);
//            return RedirectToAction(nameof(Details));
//        }
//    }
//}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    //private readonly AccountService _accountService;

    //public AccountController(AccountService accountService)
    //{
    //    _accountService = accountService;
    //}

    [Route("/account")]
    public IActionResult Details()
    {
        var viewModel = new AccoundDetailsViewModel();
        //viewModel.BasicInfo = _accountService.GetBasicInfo();
        //viewModel.AddressInfo = _accountService.GetAddressInfo();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult BasicInfo(AccoundDetailsViewModel viewModel)
    {
        //_accountService.SaveBasicInfo(viewModel.BasicInfo);
        return RedirectToAction(nameof(Details));
    }

    [HttpPost]
    public IActionResult AddressInfo(AccoundDetailsViewModel viewModel)
    {
        //_accountService.SaveAdressInfo(viewModel.AdressInfo);
        return RedirectToAction(nameof(Details));
    }
}
