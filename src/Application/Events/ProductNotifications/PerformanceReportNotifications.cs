namespace iot.Application.Events.ProductNotifications;

public class PerformanceReportNotifications : INotification
{
}

public class PerformanceSmsReportNotificationsHandler : INotificationHandler<PerformanceReportNotifications>
{
    public async Task Handle(PerformanceReportNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}


public class PerformanceEmailReportNotificationsHandler : INotificationHandler<PerformanceReportNotifications>
{
    public async Task Handle(PerformanceReportNotifications notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}