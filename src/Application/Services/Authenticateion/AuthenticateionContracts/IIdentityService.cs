using iot.Application.Common.ViewModels.Users.Authentication;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IIdentityService
{
    Task<(AccessToken Token, string Message)> SignInUserAsync(string phoneNumber, string password, CancellationToken cancellationToken);
    Task<(int Code, string Message)> SendOtpAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<(AccessToken Token, string Message)> SignInUserWithOtpAsync(int userInputOtp, string phonenumber, CancellationToken cancellationToken);
    Task<(int Code, string Message)> SignUpAndSendOtpCode(UserViewModel user, CancellationToken cancellationToken);
    Task<(AccessToken Token, string Message)> SignUpWithOtpAsync(string phonenumber, int inputUserOtpcode, CancellationToken cancellationToken);
}
