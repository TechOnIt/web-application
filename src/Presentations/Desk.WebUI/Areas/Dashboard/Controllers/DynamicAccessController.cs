using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class DynamicAccessController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
