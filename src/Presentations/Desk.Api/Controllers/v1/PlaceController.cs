using TechOnIt.Application.Common.Frameworks.ApiResultFrameWork.Filters;

namespace TechOnIt.Desk.Api.Controllers.v1;

[ApiController]
[Route("v1/[controller]/[action]")]
public class PlaceController : ControllerBase
{
    #region Ctor
    private readonly IMediator _mediator;

    public PlaceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] GetAllPlacesByFilterQuery filter)
    {
        var result = await _mediator.Send(filter);
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreatePlaceCommand place)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(place);
        return Ok(result);
    }

    [HttpPut]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdatePlaceCommand updatePlace)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(updatePlace);
        return Ok(result);
    }

    [ApiResultFilter]
    [HttpDelete("{placeId}/{structureId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] string placeId, string structureId)
    {
        var result = await _mediator
            .Send(new DeletePlaceCommand()
            {
                Id = Guid.Parse(placeId),
                StructureId = Guid.Parse(structureId)
            });

        return Ok(result);
    }
    #endregion
}
