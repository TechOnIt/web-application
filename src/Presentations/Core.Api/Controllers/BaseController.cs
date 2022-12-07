using FluentResults;
using TechOnIt.Application.Common.Models.ViewModels.Structures.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Core.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    #region Ctor & DI
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Command
    protected async Task<IActionResult> RunCommandAsync<TRequest>(TRequest request)
    {
        if (request == null)
            return NotFound();

        var resultObject = await _mediator.Send(request);

        var result = resultObject as Result<StructureAccessToken>;

        // TODO
        //var apiResult = new ApiResult(ApiResultStatusCode.Success);

        //apiResult = result.MapToApiResult();

        //if (apiResult.IsSuccess)
        //    return Ok(apiResult);
        return Ok();
    }
    #endregion

    #region Query
    protected async Task<IActionResult> RunQueryAsync(Result request)
    {
        var result = await _mediator.Send(request);

        if (result is not null)
            return Ok(result);
        return BadRequest(result);
    }
    protected async Task<IActionResult> RunQueryAsync<TResult>(Result<TResult> request)
    {
        var result = await _mediator.Send(request);

        if (result is not null)
            return Ok(result);
        return BadRequest(result);
    }
    #endregion
}