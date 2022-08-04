using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Route("v1/[controller]")]
public class RoleController : BaseController
{
    #region DI & Ctor's
    public RoleController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command

    #endregion

    #region Queries

    #endregion
}