using Infrastructure.Models;

namespace WebApp.ViewModels;

public class ContactViewModel
{
    public string Title { get; set; } = "Contact Us";
    public ContactModel Form { get; set; } = new ContactModel();
}
