namespace iot.Application.Queries;

public abstract class QueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : Query<TResult> where TResult : Result, new()
{
    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return await HandleAsync(request, cancellationToken);
    }

    public abstract Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}