namespace TechOnIt.Admin.Web.Areas.Manage.Controllers;

[Area("Manage")]
public class DashboardController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}