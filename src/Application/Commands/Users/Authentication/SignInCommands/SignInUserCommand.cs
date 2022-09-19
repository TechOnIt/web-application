using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Security.JwtBearer;
using MediatR;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserCommand : MediatR.IRequest<Result<AccessToken>>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
