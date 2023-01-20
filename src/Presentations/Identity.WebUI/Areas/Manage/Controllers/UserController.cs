namespace TechOnIt.Identity.WebUI.Areas.Manage.Controllers;

[Area("Manage")]
public class UserController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Info()
    {
        return View();
    }
}