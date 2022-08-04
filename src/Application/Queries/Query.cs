using MediatR;

namespace iot.Application.Queries;

public class Query<IResult> : IRequest<IResult> // where IResult : ApiResult
{
}