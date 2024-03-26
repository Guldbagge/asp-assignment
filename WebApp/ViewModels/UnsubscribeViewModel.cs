using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UnsubscribeViewModel
    {
        [Display(Prompt = "Enter your email", Order = 1)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
    }
}
