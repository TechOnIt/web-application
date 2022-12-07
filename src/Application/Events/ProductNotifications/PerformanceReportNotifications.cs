using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace TechOnIt.Application.Events.ProductNotifications;

public class PerformanceReportNotifications : INotification
{
}

public class PerformanceSmsReportNotificationsHandler : INotificationHandler<PerformanceReportNotifications>
{
    public async Task Handle(PerformanceReportNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}


public class PerformanceEmailReportNotificationsHandler : INotificationHandler<PerformanceReportNotifications>
{
    #region constructor
    private readonly ISmtpEmailService emailService;
    public PerformanceEmailReportNotificationsHandler(ISmtpEmailService emailService)
    {
        this.emailService = emailService;
    }

    #endregion
    public async Task Handle(PerformanceReportNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}