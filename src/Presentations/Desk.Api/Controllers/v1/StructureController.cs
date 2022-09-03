using Microsoft.AspNetCore.Mvc;

namespace Desk.Api.Controllers.v1;

public class StructureController : BaseController
{
    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken stoppingToken = default) => Ok();

    [HttpPatch]
    public async Task<IActionResult> Update(CancellationToken stoppingToken = default) => Ok();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken stoppingToken = default) => Ok();
    #endregion

    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, string keyword = null,
    CancellationToken stoppingToken = default)
    => Ok();
    #endregion
}