using TechOnIt.Application.Queries.Groups.GetAllGroupByFilter;

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

        #region Json

        [HttpGet]
        public async Task<IActionResult> GetList(int page = 1, string keyword = null,
            CancellationToken cancellationToken = default)
        {
            var paginatedGroup = await _mediator.Send(
                new GetAllGroupByFilterQuery
                {
                    Page = page,
                    PageSize = 30,
                    Keyword = keyword
                });

            return Ok();
        }

        #endregion
    }
}