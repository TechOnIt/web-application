namespace iot.Application.Commands;

public abstract class CommandHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : Command<TResult> where TResult : IResultBase, new()
{
    #region DI & Ctor
    protected readonly IMediator _mediator;

    protected CommandHandler(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    #endregion

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        TResult result;
        try
        {
            result = await HandleAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            // TODO:
            // log error.
            result = new TResult();
        }

        return result;
    }

    protected abstract Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}