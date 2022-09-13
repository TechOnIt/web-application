namespace iot.Application.Events.ProductNotifications
{
    public class SensorNotifications : INotification
    {
    }

    public class SensorSmsNotifications : INotificationHandler<SensorNotifications>
    {
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }

    public class SensorEmailNotifications : INotificationHandler<SensorNotifications>
    {
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
