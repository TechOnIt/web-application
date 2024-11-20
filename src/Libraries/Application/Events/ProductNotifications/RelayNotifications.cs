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
        private readonly ISmtpEmailSender emailService;
        public RelayEmailNotificationsHandler(ISmtpEmailSender emailService)
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