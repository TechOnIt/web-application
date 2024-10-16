using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Users.GetAllUsers;

namespace TechOnIt.Admin.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        #region DI / Ctor

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] PaginatedSearch pagination,
            CancellationToken cancellationToken)
        {
            var paginatedUsers = await _mediator.Send(new GetUsersQuery
            {
                Page = pagination.Page,
                Keyword = pagination.Keyword
            }, cancellationToken);
            return Ok(paginatedUsers);
        }
    }
}