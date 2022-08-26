namespace iot.Application.Commands.Users.Authentication.SignUpCommands;

public sealed class SignupUserCommand : IRequest<Result>
{
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? RepeatPassword { get; set; }
}
