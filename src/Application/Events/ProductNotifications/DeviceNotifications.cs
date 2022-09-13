namespace iot.Application.Events.ProductNotifications
{
    public class DeviceNotifications : INotification
    {
    }

    public class DeviceSmsNotificationsHandler : INotificationHandler<DeviceNotifications>
    {
        public async Task Handle(DeviceNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }

    public class DeviceEmailNotificationsHandler : INotificationHandler<DeviceNotifications>
    {
        public async Task Handle(DeviceNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
