//using Microsoft.AspNetCore.Mvc;

//namespace WebApp.Controllers
//{
//    public class CoursesController : Controller
//    {
//        public IActionResult Index()
//        {
//            ViewData["Title"] = "Courses";
//            return View();
//        }
//    }
//}

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
            var response = await _http.GetAsync("https://localhost:7026/api/courses");

            if (response.IsSuccessStatusCode)
            {
                viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                // Handle non-successful response
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFaild";
        }

        return View(viewModel);
    }
}

//public class CoursesController(HttpClient http) : Controller
//{
//    private readonly HttpClient _http = http;

//    public async Task<IActionResult> Index()
//    {
//        var viewModel = new CoursesIndexViewModel();

//        var response = await _http.GetAsync("https://localhost:7026/api/courses");

//        viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

//        return View(viewModel);
//    }
//}
