using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechOnIt.Application.Common.Extentions;
using TechOnIt.Application.Common.Frameworks.ApiResultFrameWork.Filters;
using TechOnIt.Application.Queries.Devices.GetAllDevicesByStructureId;

namespace TechOnIt.Board.Api.Controllers.v1;

[Authorize]
[ApiController]
[ApiResultFilter]
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
        var currentStructure = await User.GetCurrentStructureAsync();
        if (currentStructure == null)
            return Unauthorized();

        var result = await _mediatR.Send(new GetAllDevicesByStructureIdQuery { StructureId = currentStructure.StructureId });
        return Ok(result);
    }
    #endregion
}