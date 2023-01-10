using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechOnIt.Application.Common.Extentions;

namespace TechOnIt.Core.Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("v1/[controller]/[action]")]
public class DeviceController : ControllerBase
{
    #region Ctor
    private readonly IMediator _mediatR;
    public DeviceController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }
    #endregion

    #region Commands
    [HttpGet]
    public async Task<IActionResult> Command([FromQuery] Guid DeviceId, bool isOn)
    {
        return Ok();
    }
    #endregion

    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var currentStructure = User.GetCurrentStructureIdAsync();
        return Ok(currentStructure);
    }
    #endregion
}