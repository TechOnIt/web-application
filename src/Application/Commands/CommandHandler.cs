using Microsoft.Extensions.Logging;

namespace iot.Application.Commands;

public abstract class CommandHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : Command<TResult> where TResult : IResultBase, new()
{
    #region DI & Ctor
    protected readonly IMediator _mediator;
    private readonly ILogger<CommandHandler<TRequest, TResult>> _logger;
    protected CommandHandler(IMediator mediator,
        ILogger<CommandHandler<TRequest, TResult>> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    #endregion

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken = default)
    {
        TResult result;
        try
        {
            result = await HandleAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            result = new TResult();
        }

        return result;
    }

    protected abstract Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}