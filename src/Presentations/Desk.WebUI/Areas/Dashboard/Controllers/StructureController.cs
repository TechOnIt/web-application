﻿using TechOnIt.Application.Queries.Structures.GetGroupsWithRelaysByStructureId;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class StructureController : Controller
{
    #region Ctor & DI
    private readonly IMediator _mediator;
    public StructureController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    [ResponseCache(Duration = 50, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Groups([FromQuery] string structureId)
    {
        var structure = await _mediator.Send(new GetGroupsWithRelaysByStructureIdCommand { StructureId = Guid.Parse(structureId) });
        return View(structure);
    }
}