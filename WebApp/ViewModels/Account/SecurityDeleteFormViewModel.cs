using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account
{
    public class SecurityDeleteFormViewModel
    {
        [Display(Name = "Yes, I want to delete my account.", Order = 4 )]
        [CheckBoxRequired(ErrorMessage = "Please check the box before delete the account")]
        public bool IsDeleted { get; set; } = false;

    }
}
