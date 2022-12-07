using Microsoft.Extensions.Logging;

namespace TechOnIt.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region Constructor
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    #endregion

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

        var response = await next();

        _logger.LogInformation($"Handled {typeof(TResponse).Name}");

        return response;
    }
}