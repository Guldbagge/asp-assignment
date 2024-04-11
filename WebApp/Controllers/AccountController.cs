using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Views;
using WebApp.Services;
using WebApp.ViewModels.Account;
using static System.Net.WebRequestMethods;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, AddressManager addressManager, CategoryService categoryService, CourseService courseService) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;
    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;


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
        if (viewModel.SecurityPasswordForm != null)
        {
            if (viewModel.SecurityPasswordForm.CurrentPassword != null &&
                viewModel.SecurityPasswordForm.NewPassword != null &&
                viewModel.SecurityPasswordForm.ConfirmNewPassword != null)
            {
                if (viewModel.SecurityPasswordForm.NewPassword == viewModel.SecurityPasswordForm.ConfirmNewPassword)
                {
                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, viewModel.SecurityPasswordForm.CurrentPassword, viewModel.SecurityPasswordForm.NewPassword);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("IncorrectValues", "Failed to update user information");
                            ViewData["ErrorMessage"] = "Failed to update password";
                        }
                        else
                        {
                            ViewData["SuccessMessage"] = "Password updated successfully!";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("PasswordMismatch", "New password and confirm password do not match.");
                    ViewData["ErrorMessage"] = "New password and confirm password do not match.";
                }
            }
        }

        viewModel.BasicInfoForm = await PopulateBasicInfoAsync();
        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.SecurityPasswordForm = await PopulateSecurityPasswordFormAsync();

        return View(viewModel);
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


    //add message and log out coretly

    [HttpPost]
    [Route("/account/delete")]
    public async Task<IActionResult> DeleteAccount(AccountSecurityViewModel viewModel)
    {
        if (ModelState.IsValid || viewModel.SecurityDeleteForm.IsDeleted)

        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await HttpContext.SignOutAsync();
                    TempData["SuccessMessage"] = "Account deleted successfully!";
                    await Task.Delay(1000);
                    return RedirectToAction("Index", "Subscribers");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete the account";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "User not found";
            }
        }
        else
        {
            if (!viewModel.SecurityDeleteForm.IsDeleted)
            {
                ModelState.AddModelError("SecurityDeleteForm.IsDeleted", "Please check the box before deleting the account.");
            }
            return View("Security", viewModel);
        }

        return View("Security", viewModel);
    }

    #endregion

    [HttpPost]
    public async Task<IActionResult> UploadProfilImage(IFormFile file)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null && file != null && file.Length != 0)
        {
            var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/profiles", fileName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            user.ProfileImageUrl = fileName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Image uploaded successfully!";
            }
            
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to upload the image";
        }

        return RedirectToAction("Details", "Account");

    }

    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); 
            }

            var savedCourses = await _courseService.GetSavedCoursesAsync(user.Id);
        
            var viewModel = new List<UserSavedCourseModel>();
            foreach(var course in savedCourses)
            {
                var userSavedCourse = new UserSavedCourseModel
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Price = course.Price,
                    DiscountPrice = course.DiscountPrice,
                    Hours = course.Hours,
                    IsBestseller = course.IsBestseller,
                    LikesInNumbers = course.LikesInNumbers,
                    LikesInProcent = course.LikesInProcent,
                    Author = course.Author,
                    ImageName = course.ImageName
                };
                viewModel.Add(userSavedCourse);
            }

            return View(viewModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
    
            var errorMessage = "Failed to retrieve saved courses";
            TempData["ErrorMessage"] = errorMessage;
            return StatusCode(500, errorMessage);
        }
    }



    [HttpPost]
    public async Task<IActionResult> SavedCourses(UserCourseModel userCourse)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var response = await _courseService.AddCourseToSavedAsync(user.Id, userCourse.CourseId);
            if (response.IsSuccessStatusCode)
            {

                TempData["SuccessMessage"] = "Course added to saved courses successfully.";
                return RedirectToAction(nameof(SavedCourses)); 
            }
            else
            {

                TempData["ErrorMessage"] = "Failed to add course to saved courses.";
                return RedirectToAction(nameof(SavedCourses)); 
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
            TempData["ErrorMessage"] = "Failed to add course to saved courses. Please try again later.";
            return RedirectToAction(nameof(SavedCourses)); 
        }
    }


    //[HttpPost]
    //public async Task<IActionResult> SavedCourses(UserCourseModel userCourse)
    //{


    //    try
    //    {
    //        var user = await _userManager.GetUserAsync(User);
    //        if (user == null)
    //        {
    //            return Unauthorized(); // Användaren är inte inloggad
    //        }

    //        var response = await _courseService.AddCourseToSavedAsync(user.Id, userCourse.CourseId);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            return Ok(); // Kursen lades till i användarens sparade kurser
    //        }
    //        else
    //        {
    //            return BadRequest(); // Misslyckades med att lägga till kursen
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Logga felmeddelandet
    //        return StatusCode(500, "Failed to add course to saved courses. Please try again later.");
    //    }

    //}







    //public async Task<IActionResult> SavedCourses(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    //{
    //    try
    //    {
    //        var coursesResult = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);
    //        var viewModel = new CoursesIndexViewModel
    //        {
    //            Categories = await _categoryService.GetCategoriesAsync(),
    //            Courses = coursesResult.Courses,
    //            Pagination = new Pagination
    //            {
    //                PageSize = pageSize,
    //                CurrentPage = pageNumber,
    //                TotalPages = coursesResult.TotalPages,
    //                TotalItems = coursesResult.TotlaItems
    //            }
    //        };

    //        return View(viewModel);
    //    }
    //    catch (Exception)
    //    {
    //        ViewData["Status"] = "ConnectionFailed";

    //        var viewModel = new CoursesIndexViewModel
    //        {
    //        };

    //        return View(viewModel);
    //    }
    //}

    //[HttpPost]
    //[Route("/account/delete")]
    //public async Task<IActionResult> DeleteAccount()
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    if (user != null)
    //    {
    //        var result = await _userManager.DeleteAsync(user);

    //        if (result.Succeeded)
    //        {
    //            await HttpContext.SignOutAsync();
    //            ViewData["SuccessMessage"] = "Account deleted successfully!";
    //            await Task.Delay(1000);
    //            return RedirectToAction("Index", "Home");
    //        }
    //        else
    //        {
    //            ViewData["ErrorMessage"] = "Failed to delete the account";
    //        }
    //    }
    //    else
    //    {
    //        ViewData["ErrorMessage"] = "User not found";
    //    }

    //    return View("AccountDeletedConfirmation");
    //}
}
