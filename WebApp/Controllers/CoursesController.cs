
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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

    public async Task<IActionResult> Index(string category = "", string searchQuery ="")
    {
        var viewModel = new CoursesIndexViewModel
        {
            Categories = await _categoryService.GetCategoriesAsync(),
            Courses = await _courseService.GetCoursesAsync(category, searchQuery),
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {

        try
        {
            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    //[HttpPost]
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
                    ViewData["StatusCode"] = (int)response.StatusCode;
                }
            }
        }
        catch
        {
            ViewData["Status"] = "ConnectionFailed";
        }

        return View();
    }


    //[HttpPost]
    //public async Task<IActionResult> Create(CourseRegistrationFormViewModel model)
    //{
    //    try
    //    {
    //        // Kontrollera om modellen är giltig innan du försöker skapa kursen
    //        if (ModelState.IsValid)
    //        {
    //            // Skapa en ny instans av CourseModel baserat på formulärdata
    //            var newCourse = new CourseModel
    //            {
    //                Title = model.Title,
    //                Price = model.Price,
    //                DiscountPrice = model.DiscountPrice,
    //                LikesInNumbers = model.LikesInNumbers,
    //                LikesInProcent = model.LikesInProcent,
    //                Hours = model.Hours,
    //                Author = model.Author,
    //                IsBestseller = model.IsBestseller,
    //                ImageName = model.ImageName
    //            };

    //            // Använd CourseService för att skapa kursen i din databas via API:et
    //            var response = await _courseService.CreateCourseAsync(newCourse);

    //            // Om kursen skapades framgångsrikt, returnera en redirect till en passande vy
    //            if (response.IsSuccessStatusCode)
    //            {
    //                // Återställ ModelState för att undvika felmeddelanden på nästa vy
    //                ModelState.Clear();
    //                // Återvänd en lyckad status till vyn
    //                ViewData["Status"] = "Success";
    //                return RedirectToAction("Index", "Courses");
    //            }
    //            else if (response.StatusCode == HttpStatusCode.Conflict)
    //            {
    //                // Om kursen redan finns, returnera en lämplig vy med ett lämpligt meddelande
    //                ViewData["Status"] = "AlreadyExist";
    //                return View(model);
    //            }
    //            else
    //            {
    //                // Om något annat fel uppstår, returnera en lämplig vy med ett felmeddelande
    //                ViewData["Status"] = "Error";
    //                ViewData["StatusCode"] = response.StatusCode;
    //                return View(model);
    //            }
    //        }
    //        else
    //        {
    //            // Om modellen inte är giltig, returnera en vy med felmeddelanden
    //            ViewData["Status"] = "Invalid";
    //            return View(model);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Om ett undantag uppstår, hantera det och returnera en lämplig vy med ett felmeddelande
    //        ViewData["Status"] = "Error";
    //        ViewData["ErrorMessage"] = ex.Message;
    //        return View(model);
    //    }
    //}

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

