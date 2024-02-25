namespace TechOnIt.Desk.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GroupController : Controller
    {
        #region DI

        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
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
}