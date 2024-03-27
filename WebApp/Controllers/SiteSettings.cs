 using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SiteSettings : Controller
{
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
        };

        Response.Cookies.Append("ThemeMode", mode, option);

        return Ok();
    }
}
