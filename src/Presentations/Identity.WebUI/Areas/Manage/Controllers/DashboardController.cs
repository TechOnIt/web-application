using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Identity.WebUI.Areas.Manage.Controllers;

[Area("Manage")]
public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}