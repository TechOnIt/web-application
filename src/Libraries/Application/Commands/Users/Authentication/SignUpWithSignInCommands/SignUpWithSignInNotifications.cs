using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechOnIt.Application.Commands.Users.Authentication.SignUpWithSignInCommands;

public class SignUpWithSignInNotifications : INotification
{
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Link { get; set; }
}

public class SignUpWithSignInEmailNotificationHandler : INotificationHandler<SignUpWithSignInNotifications>
{
    public async Task Handle(SignUpWithSignInNotifications notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class SignUpWithSignInSmsNotificationHandler : INotificationHandler<SignUpWithSignInNotifications>
{
    public async Task Handle(SignUpWithSignInNotifications notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}