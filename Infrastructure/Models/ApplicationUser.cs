using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    [ProtectedPersonalData]

    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last Name")]
    [ProtectedPersonalData]

    public string LastName { get; set; } = null!;

}
