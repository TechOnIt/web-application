namespace iot.Application.Events.ProductNotifications;

public class StructureNotifications : INotification
{
}

public class StructureEmailNotifications : INotificationHandler<StructureNotifications>
{
    public async Task Handle(StructureNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}

public class StructureSmsNotifications : INotificationHandler<StructureNotifications>
{
    public async Task Handle(StructureNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}