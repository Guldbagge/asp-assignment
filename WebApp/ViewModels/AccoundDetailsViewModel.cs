using WebApp.Controllers;
using WebApp.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.ViewModels;

public class AccountDetailsViewModel
{
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = null!;
    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = null!;

    //public string Title { get; set; } = "Account Detaills";
    //public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
    //{
    //    //ProfileImage = "images/profile-image.svg",
    //    FirstName = "John",
    //    LastName = "Doe",
    //    Email = "jhone.doe@domain.com"
    //};

    //public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
}
