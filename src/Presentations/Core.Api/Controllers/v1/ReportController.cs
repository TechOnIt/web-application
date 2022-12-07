using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Core.Api.Controllers.v1;

[Route("v1/[controller]/[action]")]
[ApiController]
public class ReportController : BaseController
{
    #region DI & Ctor
    public ReportController(IMediator mediator)
        : base(mediator)
    {

    }
    #endregion

}
