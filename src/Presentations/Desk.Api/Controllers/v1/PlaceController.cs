using Microsoft.AspNetCore.Mvc;

namespace iot.Desk.Api.Controllers.v1;

public class PlaceController : BaseController
{
    #region Commands
    [HttpPost("{id}")]
    public async Task<IActionResult> Create(string id, CancellationToken stoppingToken = default) => Ok();

    [HttpPatch]
    public async Task<IActionResult> Update(CancellationToken stoppingToken = default) => Ok();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken stoppingToken = default) => Ok();
    #endregion

    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1,
        CancellationToken stoppingToken = default) => Ok();
    #endregion
}