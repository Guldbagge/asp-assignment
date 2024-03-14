using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account
{
    public class AccountSecurityViewModel
    {
        public SecurityPasswordFormViewModel SecurityPasswordForm { get; set; } = null!;
        public SecurityDeleteFormViewModel SecurityDeleteForm { get; set; } = null!;
        public BasicInfoFormViewModel BasicInfoForm { get; set; } = null!;
        public ProfileInfoViewModel ProfileInfo { get; set; } = null!;

    }
}
