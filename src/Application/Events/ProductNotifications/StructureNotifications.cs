using iot.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace iot.Application.Events.ProductNotifications;

public class StructureNotifications : INotification
{
}

public class StructureEmailNotifications : INotificationHandler<StructureNotifications>
{
    public async Task Handle(StructureNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public class StructureSmsNotifications : INotificationHandler<StructureNotifications>
{
    #region constructor
    private readonly ISmtpEmailService emailService;
    public StructureSmsNotifications(ISmtpEmailService emailService)
    {
        this.emailService = emailService;
    }

    #endregion
    public async Task Handle(StructureNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}