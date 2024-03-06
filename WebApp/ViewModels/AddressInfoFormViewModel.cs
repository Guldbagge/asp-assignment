using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class AddressInfoFormViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Adress line 1", Prompt = "Enter your adress line", Order = 0)]
    [Required(ErrorMessage = "Adress line is required")]
    public string StreetName { get; set; } = null!;


    [Display(Name = "Adress line 2", Prompt = "Enter your second adress line", Order = 1)]
    public string? StreetNameOptional { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
    [Required(ErrorMessage = "Postal code is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]

    public string City { get; set; } = null!;
}
