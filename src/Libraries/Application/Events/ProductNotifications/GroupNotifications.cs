namespace TechOnIt.Application.Events.ProductNotifications;

public class GroupNotifications : INotification
{
}

public class GroupSmsNotificationHandler : INotificationHandler<GroupNotifications>
{
    public async Task Handle(GroupNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public class GroupEmailNotificationHandler : INotificationHandler<GroupNotifications>
{
    #region constructor
    private readonly ISmtpEmailSender emailService;
    public GroupEmailNotificationHandler(ISmtpEmailSender emailService)
    {
        this.emailService = emailService;
    }

    #endregion
    public async Task Handle(GroupNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}