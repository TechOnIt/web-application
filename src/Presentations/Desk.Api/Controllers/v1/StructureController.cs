using iot.Application.Common.Frameworks.ApiResultFrameWork.Filters;

namespace iot.Desk.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class StructureController : ControllerBase
{
    #region constructors
    private readonly IMediator _mediator;
    private readonly IDataProtector _dataProtectionProvider;

    public StructureController(IMediator mediator, IDataProtectionProvider dataProtectionProvider)
    {
        _mediator = mediator;
        _dataProtectionProvider = dataProtectionProvider.CreateProtector("RouteData");
    }

    #endregion

    #region Commands
    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateStructureCommand structure,CancellationToken cancellationToken)
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

    [HttpDelete("{id}")]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteStructureCommand() { StructureId = Guid.Parse(id) }, cancellationToken);
        return Ok(result);
    }
    #endregion

    #region Queries

    [HttpGet]
    [ApiResultFilter]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllByFilterStructureCommand());
            return Ok(result);
        }
        catch (AppException exp)
        {
            throw new AppException(ApiResultStatusCode.ServerError, exp.Message);
        }
    }
    #endregion
}
