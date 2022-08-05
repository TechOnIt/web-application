using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("Manage"), Route("[area]/v1/[controller]/")]
public class UserController : BaseController
{
    #region DI & Ctor's
    public UserController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command

    #endregion

    #region Queries

    #endregion
}
