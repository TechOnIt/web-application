namespace iot.Application.Commands.Users.Authentication.SignUpCommands;

internal class SignUpUserSmsNotificationsHandler : INotificationHandler<SignUpUserNotification>
{
    public async Task Handle(SignUpUserNotification notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}

internal class SignUpUserEmailNotificationsHandler : INotificationHandler<SignUpUserNotification>
{
    public async Task Handle(SignUpUserNotification notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
