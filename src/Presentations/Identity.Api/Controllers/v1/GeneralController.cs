using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Controllers.v1;

[Route("v1/[controller]")]
public class GeneralController : BaseController
{
    #region DI & Ctor
    public GeneralController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command
    //[HttpPost]
    //public async Task<IActionResult> SignIn([FromBody] )
    //{

    //}
    #endregion

    #region Queries

    #endregion
}
