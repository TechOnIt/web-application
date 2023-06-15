using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IIdentityService
{
    Task<(AccessToken? Token, SigInStatus Status)?> SignInUserAsync(string username, string password,
        CancellationToken cancellationToken = default);
    Task<(string? Code, string Message)> SendOtpAsync(string phoneNumber,
        CancellationToken cancellationToken = default);
    Task<(AccessToken? Token, string Message)> VerifySignInOtpAsync(string otpCode, string phonenumber,
        CancellationToken cancellationToken = default);
    Task<(string? Code, SigInStatus Status)> SignUpAndSendOtpCode(User user,
        CancellationToken cancellationToken = default);
    Task<(AccessToken? Token, string Message)> SignUpWithOtpAsync(string phonenumber, string otpCode,
        CancellationToken cancellationToken = default);

    Task<AccessToken?> RegularSingUpAsync(User user, CancellationToken cancellationToken);
}