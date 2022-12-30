using TechOnIt.Application.Common.Frameworks.ApiResultFrameWork.Filters;

namespace TechOnIt.Desk.Api.Controllers.v1;

[ApiController]
[Route("v1/[controller]/[action]")]
public class StructureController : ControllerBase
{
    #region Ctor
    private readonly IMediator _mediator;
    private readonly IDataProtector _dataProtectionProvider;

    public StructureController(IMediator mediator, IDataProtectionProvider dataProtectionProvider)
    {
        _mediator = mediator;
        _dataProtectionProvider = dataProtectionProvider.CreateProtector("RouteData");
    }

    #endregion

    #region Queries
    [HttpGet]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllByFilterStructureCommand());
        return Ok(result);
    }
    #endregion

    #region Commands
    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateStructureCommand structure, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(structure, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateStructureCommand structure, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(structure, cancellationToken);
        return Ok(result);
    }

    [ApiResultFilter]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteStructureCommand() { StructureId = Guid.Parse(id) }, cancellationToken);
        return Ok(result);
    }
    #endregion
}