using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Sections;
using WebApp.Models.Views;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel();

        return View(viewModel);
    }



    //[Route("/dontwant")]
    //[HttpGet]
    //public IActionResult DontWant()
    //{
    //    var viewModel = new DontWantViewModel();
    //    return View(viewModel);
    //}

    //[Route("/dontwant")]
    //[HttpPost]
    //public IActionResult DontWant(DontWantViewModel viewModel)
    //{
    //    if (!ModelState.IsValid)
    //        return View(viewModel);


    //    return RedirectToAction("Index");
    //}


    //public IActionResult AccountDeletedConfirmation()
    //{
    //    return View();
    //}



    //public IActionResult Index()
    //{
    //    return View(new SubscribeViewModel());
    //}





    //[HttpPost]
    //public async Task<IActionResult> Index(DontWantViewModel viewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        using var http = new HttpClient();

    //        var url = $"https://localhost:7026/api/subscribers?email={viewModel.Form.Email}";
    //        var request = new HttpRequestMessage(HttpMethod.Post, url);

    //        var response = await http.SendAsync(request);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            //viewModel.IsSubscribed = true;
    //        }
    //    }

    //    return View(viewModel);
    //}
}





//using Microsoft.AspNetCore.Mvc;
//using WebApp.Models.Views;
//using WebApp.ViewModels;

//namespace WebApp.Controllers;

//public class HomeController : Controller
//{
//    public IActionResult Index()
//    {
//        var viewModel = new HomeIndexViewModel();

//        return View(viewModel);
//    }

//    [Route("/dontwant")]
//    [HttpGet]
//    public IActionResult DontWant()
//    {
//        var viewModel = new DontWantViewModel();
//        return View(viewModel);
//    }

//    [Route("/dontwant")]
//    [HttpPost]
//    public IActionResult DontWant(DontWantViewModel viewModel)
//    {
//        if (!ModelState.IsValid)
//            return View(viewModel);


//        return RedirectToAction("Index");
//    }

//    //public IActionResult AccountDeletedConfirmation()
//    //{
//    //    return View();
//    //}
//}


