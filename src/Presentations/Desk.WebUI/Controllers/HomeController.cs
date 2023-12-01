using System.Diagnostics;

namespace TechOnIt.Desk.WebUI.Controllers;

public class HomeController : Controller
{
    #region Ctor & DI

    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #endregion

    [HttpGet]
    public IActionResult Index()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
            return Redirect("/dashboard");
        //return Redirect("/authentication/signin");
        return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}