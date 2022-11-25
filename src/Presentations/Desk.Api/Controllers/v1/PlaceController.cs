namespace iot.Desk.Api.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class PlaceController : ControllerBase
{
    #region constructor
    private readonly IMediator _mediator;
    public PlaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion


    #region Commands
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreatePlaceCommand place)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(place);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdatePlaceCommand updatePlace)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(updatePlace);
        return Ok(result);
    }

    [HttpDelete("{placeId}/{structureId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] string placeId, string structureId)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        var result = await _mediator
            .Send( new DeletePlaceCommand() 
            { 
                Id=Guid.Parse(placeId),
                StructureId=Guid.Parse(structureId)
            });

        return Ok(result);
    }
    #endregion

    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetAll([FromBody] GetAllPlacesByFilterQuery filter)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        var result = await _mediator.Send(filter);
        return Ok(result);
    }
    #endregion
}
