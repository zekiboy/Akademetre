using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AkademetreMVC.Models;
using AkademetreMVC.Data;

namespace AkademetreMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService userService;

    public HomeController(ILogger<HomeController> logger, IUserService userService)
    {
        _logger = logger;
        this.userService = userService;
    }

    public IActionResult Index()
    {
        return View(userService.GetUsers());
    }



    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }
        
        userService.Create(user);

        return RedirectToAction("Index");
    }



    public IActionResult DownloadExcel()
    {

        var excelData = userService.GenerateExcel();
        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "user_data.xlsx");
    }






    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

