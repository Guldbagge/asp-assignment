using System.ComponentModel.DataAnnotations;
using WebApp.Helpers;

namespace WebApp.Models;

public class DontWantModel
{
    [Display(Name = "Daily Newsletter", Order = 0)]
    public bool NewsletterCheckbox1 { get; set; } = false;

    [Display(Name = "Event Updates", Order = 1)]
    public bool NewsletterCheckbox2 { get; set; } = false;

    [Display(Name = "Advertising Updates", Order = 2)]
    public bool NewsletterCheckbox3 { get; set; } = false;

    [Display(Name = "Startups Weekly", Order = 3)]
    public bool NewsletterCheckbox4 { get; set; } = false;

    [Display(Name = "Week in Review", Order = 4)]
    public bool NewsletterCheckbox5 { get; set; } = false;

    [Display(Name = "Podcasts", Order = 5)]
    public bool NewsletterCheckbox6 { get; set; } = false;

    [Display(Prompt = "Enter your email", Order = 6)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

}
