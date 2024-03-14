

using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Account;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, AddressManager addressManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }

    #region [HttpPost] Details
    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfoForm != null)
        {
            if (viewModel.BasicInfoForm.FirstName != null && viewModel.BasicInfoForm.LastName != null && viewModel.BasicInfoForm.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfoForm.FirstName;
                    user.LastName = viewModel.BasicInfoForm.LastName;
                    user.Email = viewModel.BasicInfoForm.Email;
                    user.PhoneNumber = viewModel.BasicInfoForm.PhoneNumber;
                    user.Bio = viewModel.BasicInfoForm.Biography;

                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Failed to update basic information");
                        ViewData["ErrorMessage"] = "Failed to update user information";
                    }
                    else
                    {
                        ViewData["SuccessMessage"] = "Basic information updated successfully!";
                    }
                }
            }
        }

        if (viewModel.AddressInfoForm != null)
        {
            if (viewModel.AddressInfoForm.Addressline_1 != null && viewModel.AddressInfoForm.PostalCode != null && viewModel.AddressInfoForm.City != null)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var address = await _addressManager.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.Addressline_1 = viewModel.AddressInfoForm.Addressline_1;
                        address.Addressline_2 = viewModel.AddressInfoForm.Addressline_2;
                        address.PostalCode = viewModel.AddressInfoForm.PostalCode;
                        address.City = viewModel.AddressInfoForm.City;

                        var result = await _addressManager.UpdateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Failed to update user information");
                            ViewData["ErrorMessage"] = "Failed to update address information";
                        }
                        else
                        {
                            ViewData["SuccessMessage"] = "Address information updated successfully!";
                        }

                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            UserId = user.Id,
                            Addressline_1 = viewModel.AddressInfoForm.Addressline_1,
                            Addressline_2 = viewModel.AddressInfoForm.Addressline_2,
                            PostalCode = viewModel.AddressInfoForm.PostalCode,
                            City = viewModel.AddressInfoForm.City,
                        };

                        var result = await _addressManager.CreateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Failed to update user information");
                            ViewData["ErrorMessage"] = "Failed to update address information";
                        }
                        else
                        {
                            ViewData["SuccessMessage"] = "Address information updated successfully!";
                        }
                    }
                }
            }
        }

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }

    #endregion


    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel()
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsExternalAccount = user.IsExternalAccount,
        };
    }

    private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        return new BasicInfoFormViewModel()
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Biography = user.Bio,
        };

    }

    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);
            if (address != null)
            {
                return new AddressInfoFormViewModel()
                {

                    Addressline_1 = address.Addressline_1,
                    Addressline_2 = address.Addressline_2,
                    PostalCode = address.PostalCode,
                    City = address.City,
                };
            }
        }

        return new AddressInfoFormViewModel();

    }

    #region Security

    [HttpGet]
    [Route("/account/security")]
    public async Task<IActionResult> Security()
    {
        var viewModel = new AccountSecurityViewModel
        {
            SecurityPasswordForm = await PopulateSecurityPasswordFormAsync()
        };

        viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
        viewModel.ProfileInfo ??= await PopulateProfileInfoAsync();
        return View(viewModel);
    }

    [HttpPost]
    [Route("/account/security")]
    public async Task<IActionResult> Security(AccountSecurityViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, viewModel.SecurityPasswordForm.CurrentPassword, viewModel.SecurityPasswordForm.NewPassword);

                if (result.Succeeded)
                {
                    ViewData["SuccessMessage"] = "Password updated successfully!";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("PasswordChangeError", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("UserNotFound", "User not found");
            }
        }
        else
        {
            ModelState.AddModelError("InvalidInput", "Invalid input data");
        }

        viewModel.BasicInfoForm = await PopulateBasicInfoAsync();
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.SecurityPasswordForm = await PopulateSecurityPasswordFormAsync();

        return View(viewModel);
    }

    [HttpPost]

    public IActionResult DeleteAccount(AccountSecurityViewModel viewModel)
    {
        if (TryValidateModel(viewModel.SecurityDeleteForm))
        {
            return RedirectToAction("Home", "default");
        }


        return View("Security", viewModel);
    }

    private async Task<SecurityPasswordFormViewModel> PopulateSecurityPasswordFormAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user != null)
        {
            return new SecurityPasswordFormViewModel()
            {
                UserId = user!.Id,
                ConfirmNewPassword = user.PasswordHash!,
                CurrentPassword = user.PasswordHash!,
                NewPassword = user.PasswordHash!,
            };
        }
       
        return null!;

    }




    #endregion

    //#region Security

    //[HttpGet]
    //[Route("/account/security")]
    //public async Task<IActionResult> Security()
    //{
    //    var viewModel = new AccountSecurityViewModel
    //    {
    //        SecurityPasswordForm = await PopulateSecurityPasswordFormAsync()
    //    };

    //    viewModel.BasicInfoForm ??= await PopulateBasicInfoAsync();
    //    viewModel.ProfileInfo ??= await PopulateProfileInfoAsync();
    //    return View(viewModel);
    //}

    //[HttpPost]
    //[Route("/account/security")]
    //public async Task<IActionResult> UpdateSecurityPassword(AccountSecurityViewModel viewModel)
    //{
    //    if (TryValidateModel(viewModel.SecurityPasswordForm))
    //    {
    //        return RedirectToAction("Home", "default");
    //    }


    //    return View("Security", viewModel);
    //}

    //[HttpPost]

    //public IActionResult DeleteAccount(AccountSecurityViewModel viewModel)
    //{
    //    if (TryValidateModel(viewModel.SecurityDeleteForm))
    //    {
    //        return RedirectToAction("Home", "default");
    //    }


    //    return View("Security", viewModel);
    //}

    //private async Task<SecurityPasswordFormViewModel> PopulateSecurityPasswordFormAsync()
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    if (user != null)
    //    {
    //        return new SecurityPasswordFormViewModel()
    //        {
    //            UserId = user!.Id,
    //            ConfirmNewPassword = user.PasswordHash!,
    //            CurrentPassword = user.PasswordHash!,
    //            NewPassword = user.PasswordHash!,
    //        };
    //    }

    //    return null!;

    //}




    //#endregion

}
