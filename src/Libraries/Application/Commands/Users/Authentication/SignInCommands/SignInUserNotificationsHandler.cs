﻿namespace TechOnIt.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserSmsNotificationsHandler : INotificationHandler<SignInUserNotifications>
{
    public async Task Handle(SignInUserNotifications notification, CancellationToken cancellationToken = default)
    {
        //codes to do for send sms notification
        await Task.CompletedTask;
    }
}

public class SignInUserEmailNotificationsHandler : INotificationHandler<SignInUserNotifications>
{
    public async Task Handle(SignInUserNotifications notification, CancellationToken cancellationToken = default)
    {
        //codes to do for send email notification
        await Task.CompletedTask;
    }
}
