using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Controllers.v1;

[Route("v1/[controller]")]
public class AuthenticationController : BaseController
{
    #region DI & Ctor
    public AuthenticationController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command
    //[HttpPost("signin")]
    //public async Task Signin()
    //    => await RunCommandAsync();
    #endregion

    #region Queries

    #endregion
}
