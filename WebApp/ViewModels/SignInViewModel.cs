

//using Infrastructure.Models;

//namespace WebApp.ViewModels;

//public class SignInViewModel
//{
//    public string Title { get; set; } = "Sign In";
//    public SignInModel Form { get; set; } = new SignInModel();
//    public string? ErrorMessage { get; set; }
//}

using Infrastructure.Models;

namespace WebApp.ViewModels;
public class SignInViewModel
{
    public string Title { get; set; } = "Sign In";
    public SignInModel Form { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }

    //// Nytt från SignInModel
    //public string Email { get; set; }
    //public string Password { get; set; }
    //public bool RememberMe { get; set; }
    public string Password => Form.Password;
    public string Email => Form.Email;
 public bool RememberMe => Form.RememberMe;
}
