using System.Diagnostics;
using TechOnIt.Application.Handlers;
using TechOnIt.Desk.Web.DynamicAccess;

namespace TechOnIt.Desk.Web.Controllers;

public class HomeController : Controller
{
    #region Ctor & DI

    private readonly ILogger<HomeController> _logger;
    private readonly AreaService _areaService;
    public HomeController(ILogger<HomeController> logger, AreaService areaService)
    {
        _logger = logger;
        _areaService = areaService;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
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