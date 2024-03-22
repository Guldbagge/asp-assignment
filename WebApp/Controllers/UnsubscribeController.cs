﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpPut]
    public async Task<IActionResult> Index(UnsubscribeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Skicka en begäran till API:et för att avprenumerera användaren
                // Om användaren inte finns i databasen, returnera en felmeddelande
                // Om användaren finns i databasen, avprenumerera användaren och returnera en bekräftelsesida
                using var http = new HttpClient();
                var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
                var url = $"https://localhost:7026/api/subscribers?key={apiKey}&email={viewModel.Email}";

                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = await http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewData["Status"] = "NotFound"; // Lägg till denna rad för att hantera NotFound-statuskoden
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewData["Status"] = "Unauthorized";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }

        return View("Index", viewModel);
    }



    //public IActionResult UnsubscribeConfirmation()
    //{
    //    // Visa en bekräftelsesida för användaren efter avprenumerationen
    //    return View();
    //}
}
