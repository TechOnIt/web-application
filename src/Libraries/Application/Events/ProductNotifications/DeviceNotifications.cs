﻿using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;

namespace TechOnIt.Application.Events.ProductNotifications
{
    public class DeviceNotifications : INotification
    {
    }

    public class DeviceSmsNotificationsHandler : INotificationHandler<DeviceNotifications>
    {
        public async Task Handle(DeviceNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }

    public class DeviceEmailNotificationsHandler : INotificationHandler<DeviceNotifications>
    {
        #region constructor
        private readonly ISmtpEmailService emailService;
        public DeviceEmailNotificationsHandler(ISmtpEmailService emailService)
        {
            this.emailService = emailService;
        }

        #endregion

        public async Task Handle(DeviceNotifications notification, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}