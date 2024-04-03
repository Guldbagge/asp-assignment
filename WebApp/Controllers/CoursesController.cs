
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using WebApp.Models;
using WebApp.Models.Views;
using WebApp.Services;

namespace WebApp.Controllers;

[Authorize]

public class CoursesController(CategoryService categoryService, CourseService courseService) : Controller
{


    private readonly CategoryService _categoryService = categoryService;
    private readonly CourseService _courseService = courseService;

    public async Task<IActionResult> Index()
    {
        var viewModel = new CoursesIndexViewModel
        {
            Categories = await _categoryService.GetCategoriesAsync(),
            Courses = await _courseService.GetCoursesAsync(),
        };

        return View(viewModel);
    }
}


    //private readonly HttpClient _http;

    //public CoursesController(HttpClient http)
    //{
    //    _http = http;
    //}

    //public async Task<IActionResult> Index()
    //{
    //    var viewModel = new CoursesIndexViewModel();

    //    try
    //    {
    //        //var response = await _http.GetAsync("https://localhost:7026/api/courses");
    //        var response = await _http.GetAsync("https://localhost:7026/api/courses?key=5e29b885-1414-4046-bb3c-33ac9c611b01");

    //        if (response.IsSuccessStatusCode)
    //        {
    //            viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;
    //        }
    //        else
    //        {
    //            ViewData["Status"] = "ConnectionFailed";
    //        }
    //    }
    //    catch
    //    {
    //        ViewData["Status"] = "ConnectionFailed";
    //    }

    //    return View(viewModel);
    //}

    //public async Task<IActionResult> Details(int id)
    //{
    //    var viewModel = new CoursesIndexViewModel();

    //    try
    //    {
    //        var response = await _http.GetAsync($"https://localhost:7026/api/courses/{id}?key=5e29b885-1414-4046-bb3c-33ac9c611b01");

    //        if (response.IsSuccessStatusCode)
    //        {
    //           var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync())!;
    //           return View(course);
    //        }
    //        else
    //        {
    //            ViewData["Status"] = "ConnectionFailed";
    //        }
    //    }
    //    catch
    //    {
    //        ViewData["Status"] = "ConnectionFailed";
    //    }

    //    return View(viewModel);
    //}

