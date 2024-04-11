using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Contact Us";
        return View(new ContactViewModel());
    }


    [HttpPost]
    public async Task<IActionResult> Index(ContactViewModel viewModel)
    {
        if (viewModel != null && ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
                var url = "https://localhost:7026/api/contact?key=" + apiKey;

                var json = JsonSerializer.Serialize(viewModel.Form);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await http.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Success";
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    TempData["StatusCode"] = (int)response.StatusCode;
                    TempData["Status"] = "Unauthorized";
                }
                else
                {
                    TempData["StatusCode"] = (int)response.StatusCode;
                    TempData["Status"] = "Error";
                }
            }
            catch (HttpRequestException)
            {
                TempData["Status"] = "ConnectionFailed";
            }
        }
        else
        {
            TempData["Status"] = "InvalidInput";
        }

        return RedirectToAction("Index", "Contact");
    }




    //[HttpPost]
    //public async Task<IActionResult> Index(ContactViewModel viewModel)
    //{
    //    if (viewModel != null && ModelState.IsValid)
    //    {
    //        try
    //        {
    //            using var http = new HttpClient();
    //            var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
    //            var url = "https://localhost:7026/api/contact?key=" + apiKey;

    //            var json = JsonSerializer.Serialize(viewModel);

    //            var content = new StringContent(json, Encoding.UTF8, "application/json");

    //            var response = await http.PostAsync(url, content);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                TempData["Status"] = "Success";
    //            }
    //            else if (response.StatusCode == HttpStatusCode.Unauthorized)
    //            {
    //                TempData["StatusCode"] = (int)response.StatusCode;
    //                TempData["Status"] = "Unauthorized";
    //            }
    //            else
    //            {
    //                TempData["StatusCode"] = (int)response.StatusCode;
    //                TempData["Status"] = "Error";
    //            }
    //        }
    //        catch (HttpRequestException)
    //        {
    //            TempData["Status"] = "ConnectionFailed";
    //        }
    //    }
    //    else
    //    {
    //        TempData["Status"] = "InvalidInput";
    //    }

    //    return RedirectToAction("Index", "Contact");
    //}



    //[HttpPost]
    //public async Task<IActionResult> Index(ContactViewModel viewModel)
    //{
    //    if (viewModel != null && ModelState.IsValid)
    //    {
    //        try
    //        {
    //            using var http = new HttpClient();
    //            var apiKey = "5e29b885-1414-4046-bb3c-33ac9c611b01";
    //            var url = $"https://localhost:7026/api/contact?key={apiKey}&fullname={viewModel.Form.FullName}&email={viewModel.Form.Email}&service={viewModel.Form.SelectedService}&message={viewModel.Form.Message}";

    //            var request = new HttpRequestMessage(HttpMethod.Post, url);
    //            var response = await http.SendAsync(request);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                TempData["Status"] = "Success";
    //            }
    //            else if (response.StatusCode == HttpStatusCode.Unauthorized)
    //            {
    //                TempData["StatusCode"] = (int)response.StatusCode;
    //                TempData["Status"] = "Unauthorized";
    //            }
    //            else
    //            {
    //                TempData["StatusCode"] = (int)response.StatusCode;
    //                TempData["Status"] = "Error";
    //            }
    //        }
    //        catch (HttpRequestException)
    //        {
    //            TempData["Status"] = "ConnectionFailed";
    //        }
    //    }
    //    else
    //    {
    //        TempData["Status"] = "InvalidInput";
    //    }

    //    return RedirectToAction("Index", "Contact");
    //}



}
