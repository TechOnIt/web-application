using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Structures.GetAllByFilter;

namespace TechOnIt.Admin.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class StructureController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public StructureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PaginatedSearch paginatedSearch, CancellationToken cancellationToken)
        {
            var structures = await _mediator.Send(new GetAllByFilterStructureQuery(),
                cancellationToken);
            return View(structures);
        }
        #endregion
    }
}