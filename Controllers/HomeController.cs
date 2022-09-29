using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models.GiaiPT;

namespace DemoMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GiaiPTbac1(){
        return View();
    }
    public IActionResult GiaiPTbac2(){
        return View();
    }
    [HttpPost]
    public IActionResult GiaiPTbac1(string a, string b){
        ViewData["Result"] = GiaiPT.GiaiPTbac1(a, b);
        return View();
    }
    [HttpPost]
    public IActionResult GiaiPTbac2(string a, string b, string c){
        ViewData["Result"] = GiaiPT.GiaiPTbac2(a, b, c);
        return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
