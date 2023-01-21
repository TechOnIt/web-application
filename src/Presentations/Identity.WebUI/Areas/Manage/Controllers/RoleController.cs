namespace TechOnIt.Identity.WebUI.Areas.Manage.Controllers;

[Area("Manage")]
public class RoleController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}