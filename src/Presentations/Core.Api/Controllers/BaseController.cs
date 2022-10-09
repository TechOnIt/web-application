using FluentResults;
using iot.Application.Commands;
using iot.Application.Common.Extentions;
using iot.Application.Common.Models;
using iot.Application.Common.ViewModels.Structures.Authentication;
using iot.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot.Core.Api.Controllers;

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
    protected async Task<IActionResult> RunQueryAsync(Query<Result> request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    protected async Task<IActionResult> RunQueryAsync<TResult>(Query<Result<TResult>> request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    #endregion
}