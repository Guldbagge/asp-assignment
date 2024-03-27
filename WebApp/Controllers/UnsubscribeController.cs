using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Sections;

namespace WebApp.Controllers;

public class UnsubscribeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UnsubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
                var url = $"https://localhost:7026/api/subscribers?key={apiKey}&email={viewModel.Email}";
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = await http.SendAsync(request);

                ViewData["StatusCode"] = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewData["Status"] = "NotFound";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewData["Status"] = "Unauthorized";
                }
                else
                {
                    ViewData["Status"] = "ServerSent";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }

        return View(viewModel);
    }

}
