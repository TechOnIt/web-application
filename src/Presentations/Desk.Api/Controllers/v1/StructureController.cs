using iot.Application.Commands.Structures.Management.CreateStructure;
using iot.Application.Commands.Structures.Management.DeleteStructure;
using iot.Application.Commands.Structures.Management.UpdateStructure;
using iot.Application.Common.Exceptions;
using iot.Application.Common.Models;
using iot.Application.Queries.Structures.GetAllByFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot.Desk.Api.Controllers.v1;

public class StructureController : BaseController
{
    #region constructors
    private readonly IMediator _mediator;
    public StructureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Commands
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateStructureCommand structure)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cancellation = new CancellationToken();
            var result = await _mediator.Send(structure, cancellation);
            return Ok(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    [HttpPatch]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateStructureCommand structure)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stoppingToken = new CancellationToken();
            var result = await _mediator.Send(structure, stoppingToken);

            return Ok(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    [HttpDelete("{id}")]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string id)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        try
        {
            var cancellOperation = new CancellationToken();
            var result = await _mediator.Send(new DeleteStructureCommand() { StructureId = Guid.Parse(id) });
            return Ok(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
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