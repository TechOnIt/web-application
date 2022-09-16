using Microsoft.AspNetCore.Mvc;

namespace iot.Core.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CommandController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCommands()
        => Ok(); // for example...
}