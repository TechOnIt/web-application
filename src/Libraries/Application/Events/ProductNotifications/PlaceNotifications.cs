using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace TechOnIt.Application.Events.ProductNotifications;

public class PlaceNotifications : INotification
{
}

public class PlaceSmsNotificationHandler : INotificationHandler<PlaceNotifications>
{
    public async Task Handle(PlaceNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}

public class PlaceEmailNotificationHandler : INotificationHandler<PlaceNotifications>
{
    #region constructor
    private readonly ISmtpEmailService emailService;
    public PlaceEmailNotificationHandler(ISmtpEmailService emailService)
    {
        this.emailService = emailService;
    }

    #endregion
    public async Task Handle(PlaceNotifications notification, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}