﻿using TechOnIt.Application.Commands.Structures.Dashboard.GetMyStructures;
using TechOnIt.Application.Common.Extentions;
using TechOnIt.Application.Queries.Structures.GetPlacesWithDevicesByStructureId;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class StructureController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public StructureController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var currentUser = await User.GetCurrentUserIdAsync();
        var myStructures = await _mediator.Send(new GetMyStructuresCardCommand { UserId = currentUser.Id });
        return View(myStructures);
    }

    [HttpGet]
    public async Task<IActionResult> Places([FromQuery] string structureId)
    {
        var structure = await _mediator.Send(new GetPlacesWithDevicesByStructureIdCommand { StructureId = Guid.Parse(structureId) });
        return View(structure);
    }
}