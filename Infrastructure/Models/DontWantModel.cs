//using System.ComponentModel.DataAnnotations;
//using Infrastructure.Helpers;

//namespace WebApp.Models;

//public class DontWantModel
//{
//    [Display(Name = "Daily Newsletter", Order = 0)]
//    public bool NewsletterCheckbox1 { get; set; } 

//    [Display(Name = "Event Updates", Order = 1)]
//    public bool NewsletterCheckbox2 { get; set; } 

//    [Display(Name = "Advertising Updates", Order = 2)]
//    public bool NewsletterCheckbox3 { get; set; } 

//    [Display(Name = "Startups Weekly", Order = 3)]
//    public bool NewsletterCheckbox4 { get; set; }

//    [Display(Name = "Week in Review", Order = 4)]
//    public bool NewsletterCheckbox5 { get; set; } 

//    [Display(Name = "Podcasts", Order = 5)]
//    public bool NewsletterCheckbox6 { get; set; } 

//    [Display(Prompt = "Enter your email", Order = 6)]
//    [DataType(DataType.EmailAddress)]
//    [Required(ErrorMessage = "Email is required")]
//    public string Email { get; set; } = null!;

//}
