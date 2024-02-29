

//using Infrastructure.Models;

//namespace WebApp.ViewModels;

//public class SignUpViewModel
//{
//    public string Title { get; set; } = "Sign Up";
//    public SignUpModel Form { get; set; } = new SignUpModel();
//}

using Infrastructure.Models;

namespace WebApp.ViewModels
{
    public class SignUpViewModel
    {
        public string Title { get; set; } = "Sign Up";
        public SignUpModel Form { get; set; } = new SignUpModel();

        // Dessa två rader behöver läggas till
        public string FirstName => Form.FirstName;
        public string LastName => Form.LastName;
        public string Email => Form.Email;
        public string Password => Form.Password;
    }
}
