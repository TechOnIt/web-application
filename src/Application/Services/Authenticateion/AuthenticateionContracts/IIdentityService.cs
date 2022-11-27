using iot.Application.Common.ViewModels.Users;
using iot.Application.Common.ViewModels.Users.Authentication;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IIdentityService
{
    Task<(AccessToken Token, string Message)?> SignInUserAsync(string phoneNumber, string password,
        CancellationToken cancellationToken = default);
    Task<(string? Code, string Message)> SendOtpAsync(string phoneNumber,
        CancellationToken cancellationToken = default);
    Task<(AccessToken Token, string Message)> VerifySignInOtpAsync(string otpCode, string phonenumber,
        CancellationToken cancellationToken = default);
    Task<(string? Code, string Message)> SignUpAndSendOtpCode(UserViewModel user,
        CancellationToken cancellationToken = default);
    Task<(AccessToken Token, string Message)> SignUpWithOtpAsync(string phonenumber, string otpCode,
        CancellationToken cancellationToken = default);
}