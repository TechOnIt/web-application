using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

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
    private readonly ISmtpEmailService _emailService;
    public SensorReportEmailNotificationsHandler(ISmtpEmailService emailService)
    {
        _emailService = emailService;
    }
    #endregion

    public async Task Handle(SensorReportNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}