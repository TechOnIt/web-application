using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Interfaces;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public sealed class SignInUserCommand : IRequest<Result<AccessToken>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
