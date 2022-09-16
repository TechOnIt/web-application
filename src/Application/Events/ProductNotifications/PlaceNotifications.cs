namespace iot.Application.Events.ProductNotifications;

public class PlaceNotifications : INotification
{
}

public class PlaceSmsNotificationHandler : INotificationHandler<PlaceNotifications>
{
    public async Task Handle(PlaceNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}

public class PlaceEmailNotificationHandler : INotificationHandler<PlaceNotifications>
{
    public async Task Handle(PlaceNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}