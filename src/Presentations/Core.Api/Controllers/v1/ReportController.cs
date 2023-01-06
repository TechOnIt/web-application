using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Core.Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("v1/[controller]/[action]")]
public class ReportController : ControllerBase
{
    #region DI & Ctor
    private readonly IMediator _mediator;
    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Queries

    #endregion

    #region Commands

    #endregion
}
