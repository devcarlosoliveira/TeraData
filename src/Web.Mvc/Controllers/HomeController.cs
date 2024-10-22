using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Web.Mvc.Domain.Base;
using Web.Mvc.Models;

namespace Web.Mvc.Controllers;

public class HomeController : BaseController
{
    //private readonly ILogger<HomeController> _logger;

    public HomeController(IAppIdentityUser user) : base(user)
    {
    }

    public IActionResult Index()
    {
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
