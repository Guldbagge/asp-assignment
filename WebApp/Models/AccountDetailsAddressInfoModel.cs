using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountDetailsAddressInfoModel
{
    [Display(Name = "Adress line 1", Prompt = "Enter your adress line", Order = 0)]
    [Required(ErrorMessage = "Adress line is required")]
    public string Adressline_1 { get; set; } = null!;


    [Display(Name = "Adress line 2", Prompt = "Enter your second adress line", Order = 1)]
    public string? Adressline_2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
    [Required(ErrorMessage = "Postal code is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;


    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
   
    public string City { get; set; } = null!;
}
