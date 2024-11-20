using TechOnIt.Application.Common.Interfaces.Clients.Notifications;

namespace TechOnIt.Application.Events.ProductNotifications
{
    public class SensorNotifications : INotification
    {
    }

    public class SensorSmsNotifications : INotificationHandler<SensorNotifications>
    {
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }

    public class SensorEmailNotifications : INotificationHandler<SensorNotifications>
    {
        #region constructor
        private readonly ISmtpEmailSender emailService;
        public SensorEmailNotifications(ISmtpEmailSender emailService)
        {
            this.emailService = emailService;
        }

        #endregion
        public async Task Handle(SensorNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}
