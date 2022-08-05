namespace Application.Events;

public abstract class NotificationBaseHandler<TRequest> : INotificationHandler<TRequest>
    where TRequest : INotification
{
    public async Task Handle(TRequest notification, CancellationToken cancellationToken)
    {
        await HandleAsync(notification, cancellationToken);
    }

    protected abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}