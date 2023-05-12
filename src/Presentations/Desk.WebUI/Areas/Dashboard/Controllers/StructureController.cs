using TechOnIt.Application.Queries.Structures.GetPlacesWithDevicesByStructureId;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class StructureController : Controller
{
    private readonly IMediator _mediator;
    public StructureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    [ResponseCache(Duration = 50, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Places([FromQuery] string structureId)
    {
        var structure = await _mediator.Send(new GetPlacesWithDevicesByStructureIdCommand { StructureId = Guid.Parse(structureId) });
        return View(structure);
    }
}