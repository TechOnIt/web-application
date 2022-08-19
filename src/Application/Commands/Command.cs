using FluentResults;
using MediatR;

namespace iot.Application.Commands;

public abstract class Command<TResult> : IRequest<TResult>  where TResult : IResultBase
{
}
