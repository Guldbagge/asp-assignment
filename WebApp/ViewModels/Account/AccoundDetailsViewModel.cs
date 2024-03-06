using WebApp.Controllers;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public ProfileInfoViewModel? ProfileInfo { get; set; }
    public BasicInfoFormViewModel? BasicInfoForm { get; set; }
    public AddressInfoFormViewModel? AddressInfoForm { get; set; }

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
