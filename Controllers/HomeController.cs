using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeiChenMidTermTest.Models;

namespace LeiChenMidTermTest.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
  }

  public void setCookies()
  {
    var userName = System.Environment.MachineName;
    Console.WriteLine("UserName: " + userName);
    HttpContext.Response.Cookies.Append("User", userName);
  }
  public IActionResult Index()
  {
    setCookies();

    return View();
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
