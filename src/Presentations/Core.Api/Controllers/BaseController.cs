﻿using FluentResults;
using iot.Application.Commands;
using iot.Application.Common.Interfaces;
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
        where TRequest : class
    {
        var result = await _mediator.Send(request) as Result;

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest();
    }

    protected async Task<IActionResult> RunCommandAsync(Command<Result> request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest();
    }

    protected async Task<IActionResult> RunCommandAsync<TResult>(Command<Result<TResult>> request)
    {
        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest();
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