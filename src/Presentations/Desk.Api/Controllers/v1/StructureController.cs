using iot.Application.Commands.Structures.Management.CreateStructure;
using iot.Application.Commands.Structures.Management.DeleteStructure;
using iot.Application.Commands.Structures.Management.UpdateStructure;
using iot.Application.Common.Exceptions;
using iot.Application.Common.Frameworks.ApiResultFrameWork;
using iot.Application.Queries.Structures.GetAllByFilter;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace iot.Desk.Api.Controllers.v1;

public class StructureController : BaseController
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
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateStructureCommand structure)
    {
        if (User.Identity != null)
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

        var cancellation = new CancellationToken();
        var result = await _mediator.Send(structure, cancellation);
        return Ok(result);
    }

    [HttpPatch]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateStructureCommand structure)
    {
        if (User.Identity != null)
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

        var stoppingToken = new CancellationToken();
        var result = await _mediator.Send(structure, stoppingToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id)
    {
        if (User.Identity != null)
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

        var result = await _mediator.Send(new DeleteStructureCommand() { StructureId = Guid.Parse(id) });
        return Ok(result);
    }
    #endregion

    #region Queries

    [HttpGet]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetAll()
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

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