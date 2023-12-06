using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using TechOnIt.Application.DesignPatterns.Builder.DynamicAccessBuilder;

namespace TechOnIt.Desk.Web.Controllers;

public class HomeController : Controller
{
    #region Ctor & DI

    private readonly ILogger<HomeController> _logger;
    private readonly IActionDescriptorCollectionProvider _provider;
    public HomeController(ILogger<HomeController> logger, IActionDescriptorCollectionProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var builder = new AreaControllerActionBuilder();
        var director = new AreaControllerActionDirector(builder);
        var controllerActions = director.Construct(_provider);


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