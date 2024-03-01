using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Factories;
using Infrastructure.Repositoties;

namespace Infrastructure.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseResult> UpdateAccountAsync(UserUpdateModel model)
        {
            try
            {
                var userId = "user id from token";
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return ResponseFactory.NotFound();

                if (!string.IsNullOrEmpty(model.FirstName))
                    user.FirstName = model.FirstName;

                if (!string.IsNullOrEmpty(model.LastName))
                    user.LastName = model.LastName;

                if (!string.IsNullOrEmpty(model.Email))
                    user.Email = model.Email;

                if (!string.IsNullOrEmpty(model.Phone))
                    user.PhoneNumber = model.Phone;

                //if (!string.IsNullOrEmpty(model.Biography))
                //    user.Biography = model.Biography;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return ResponseFactory.Error(string.Join("\n", result.Errors.Select(e => e.Description)));
                }

                return ResponseFactory.Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
