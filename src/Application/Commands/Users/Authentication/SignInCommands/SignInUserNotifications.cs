namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserNotifications : INotification
{
    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Otp { get; set; }
    public string? Token { get; set; }
}
