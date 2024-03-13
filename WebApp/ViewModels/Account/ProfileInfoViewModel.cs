namespace WebApp.ViewModels.Account;

public class ProfileInfoViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ProfileImageUrl { get; set; } = "profile-image.svg";

    public bool IsExternalAccount { get; set; }

    public BasicInfoFormViewModel? BasicInfoForm { get; set; }
}