using iot.Application.Common.DTOs.Users.Authentication;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IIdentityService
{
    Task<(AccessToken Token, string Message)> SignInUserAsync(string phoneNumber, string password, CancellationToken cancellationToken);
}
