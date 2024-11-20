using TechOnIt.Application.Common.Interfaces.Clients.Notifications;

namespace TechOnIt.Application.Events.ProductNotifications;

public class SensorReportNotifications : INotification
{
}

public class SensorReportSmsNotificationsHandler : INotificationHandler<SensorReportNotifications>
{
    public async Task Handle(SensorReportNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public class SensorReportEmailNotificationsHandler : INotificationHandler<SensorReportNotifications>
{
    #region Ctor
    private readonly ISmtpEmailSender _emailService;
    public SensorReportEmailNotificationsHandler(ISmtpEmailSender emailService)
    {
        _emailService = emailService;
    }
    #endregion

    public async Task Handle(SensorReportNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}