using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Views;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
       
        var viewModel = new HomeIndexViewModel();

        ViewData["Title"] = viewModel.Title;
        return View(viewModel);
    }

    [Route("/dontwant")]
    [HttpGet]
    public IActionResult DontWant()
    {
        var viewModel = new DontWantViewModel();
        return View(viewModel);
    }

    [Route("/dontwant")]
    [HttpPost]
    public IActionResult DontWant(DontWantViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);


        return RedirectToAction("Home");
    }
}


