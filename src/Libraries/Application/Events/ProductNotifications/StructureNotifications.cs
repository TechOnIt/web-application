using TechOnIt.Application.Common.Interfaces.Clients.Notifications;

namespace TechOnIt.Application.Events.ProductNotifications;

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
    private readonly ISmtpEmailSender emailService;
    public StructureSmsNotifications(ISmtpEmailSender emailService)
    {
        this.emailService = emailService;
    }

    #endregion
    public async Task Handle(StructureNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}