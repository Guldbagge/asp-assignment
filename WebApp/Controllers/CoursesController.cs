
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using WebApp.Models;
using WebApp.Models.Views;

namespace WebApp.Controllers;

[Authorize]

public class CoursesController : Controller
{
    private readonly HttpClient _http;

    public CoursesController(HttpClient http)
    {
        _http = http;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new CoursesIndexViewModel();

        try
        {
            //var response = await _http.GetAsync("https://localhost:7026/api/courses");
            var response = await _http.GetAsync("https://localhost:7026/api/courses?key=5e29b885-1414-4046-bb3c-33ac9c611b01");

            if (response.IsSuccessStatusCode)
            {
                viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFailed";
        }

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var viewModel = new CoursesIndexViewModel();

        try
        {
            var response = await _http.GetAsync($"https://localhost:7026/api/courses/{id}?key=5e29b885-1414-4046-bb3c-33ac9c611b01");

            if (response.IsSuccessStatusCode)
            {
               var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync())!;
               return View(course);
            }
            else
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFailed";
        }

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> Create(CourseRegistrationFormViewModel viewModel)
    //{
    //    try
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            using var http = new HttpClient();

    //            var json = JsonConvert.SerializeObject(viewModel);
    //            using var content = new StringContent(json, Encoding.UTF8, "application/json");
    //            var response = await http.PostAsync($"https://localhost:7026/api/courses", content);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                ViewData["Status"] = "CourseCreated";
    //            }
    //            else
    //            {
    //                ViewData["Status"] = "ConnectionFailed"; 
    //            }
    //        }
    //    }

    //    catch
    //    {
    //        ViewData["Status"] = "ConnectionFailed";
    //    }

    //    return View();
    //}
    [HttpPost]
    public async Task<IActionResult> Create(CourseRegistrationFormViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();

                var json = JsonConvert.SerializeObject(viewModel);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"https://localhost:7026/api/courses", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else
                {
                    ViewData["Status"] = "ConnectionFailed";
                    ViewData["StatusCode"] = (int)response.StatusCode; // Store status code
                }
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFailed";
        }

        return View();
    }

}
