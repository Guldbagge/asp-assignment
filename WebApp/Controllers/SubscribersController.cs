using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models;
using WebApp.Models.Sections;

namespace WebApp.Controllers;

public class SubscribersController : Controller
{
    public IActionResult Index()
    {
        return View(new SubscribeViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(SubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var url = $"https://localhost:7026/api/subscribers?email={viewModel.Email}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var response = await http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                viewModel.IsSubscribed = true;
            }
        }

        return View(viewModel);
    }
}
