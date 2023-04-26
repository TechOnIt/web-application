namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class ProfileController : Controller
{
    private IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}