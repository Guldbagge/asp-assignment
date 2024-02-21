using WebApp.Models;

namespace WebApp.ViewModels;

public class DontWantViewModel
{
    public string Title { get; set; } = "Subscribe*";
    public DontWantModel Form { get; set; } = new DontWantModel();
}
