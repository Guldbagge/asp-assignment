
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
}
