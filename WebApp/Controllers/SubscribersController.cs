using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
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
            try
            {

                using var http = new HttpClient();
                var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
                var url = $"https://localhost:7026/api/subscribers?key={apiKey}&email={viewModel.Email}&newsletterCheckbox1={viewModel.NewsletterCheckbox1}&newsletterCheckbox2={viewModel.NewsletterCheckbox2}&newsletterCheckbox3={viewModel.NewsletterCheckbox3}&newsletterCheckbox4={viewModel.NewsletterCheckbox4}&newsletterCheckbox5={viewModel.NewsletterCheckbox5}&newsletterCheckbox6={viewModel.NewsletterCheckbox6}&isSubscribed={viewModel.IsSubscribed}";

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var response = await http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Success";
                    viewModel.IsSubscribed = true;
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["Status"] = "AlreadyExist";
                }


                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["Status"] = "Unauthorized";
                }

            }

            catch
            {
                TempData["Status"] = "ConnectionFailed";
            }

        }
        return RedirectToAction("Index", "Home", "dontWant");
    }



    //[HttpPost]

    //public async Task<IActionResult> Index(SubscribeViewModel viewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {

    //            using var http = new HttpClient();
    //            var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
    //            var url = $"https://localhost:7026/api/subscribers?key={apiKey}&email={viewModel.Email}&newsletterCheckbox1={viewModel.NewsletterCheckbox1}&newsletterCheckbox2={viewModel.NewsletterCheckbox2}&newsletterCheckbox3={viewModel.NewsletterCheckbox3}&newsletterCheckbox4={viewModel.NewsletterCheckbox4}&newsletterCheckbox5={viewModel.NewsletterCheckbox5}&newsletterCheckbox6={viewModel.NewsletterCheckbox6}&isSubscribed={viewModel.IsSubscribed}";

    //            var request = new HttpRequestMessage(HttpMethod.Post, url);
    //            var response = await http.SendAsync(request);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                ViewData["Status"] = "Success";
    //                viewModel.IsSubscribed = true;
    //                //return RedirectToAction("Index", "Home");
    //            }

    //            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
    //            {
    //                ViewData["Status"] = "AlreadyExist";
    //            }


    //            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    //            {
    //                ViewData["Status"] = "Unauthorized";
    //            }

    //        }

    //        catch
    //        {
    //            ViewData["Status"] = "ConnectionFailed";
    //        }

    //    }

    //    return View(viewModel);
    //    //return RedirectToAction("Index", "Home");
    //}
}
