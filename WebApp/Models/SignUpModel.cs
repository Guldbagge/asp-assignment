using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Models;

public class SignUpModel
{
    [Display(Name = "First Name", Prompt = "Enter your frist name", Order = 0)]
    [Required (ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^(?=.*[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,})[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
                          ErrorMessage = "The email format is not valid.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s])(?=.*[a-zA-Z\d]).{8,}$",
                      ErrorMessage = "The password format is not valid. It must contain at least one uppercase letter, one digit, one special character, and be at least 8 characters long.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "I agree to the Terms and Conditions", Order = 5)]
    [Required(ErrorMessage = "You must agree to the terms and conditions")]
    public bool TermsAndConditions { get; set; } = false;
}

