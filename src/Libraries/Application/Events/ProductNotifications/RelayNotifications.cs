using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace TechOnIt.Application.Events.ProductNotifications
{
    public class RelayNotifications : INotification
    {
    }

    public class RelaySmsNotificationsHandler : INotificationHandler<RelayNotifications>
    {
        public async Task Handle(RelayNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }

    public class RelayEmailNotificationsHandler : INotificationHandler<RelayNotifications>
    {
        #region constructor
        private readonly ISmtpEmailService emailService;
        public RelayEmailNotificationsHandler(ISmtpEmailService emailService)
        {
            this.emailService = emailService;
        }

        #endregion

        public async Task Handle(RelayNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}