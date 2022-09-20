using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Extentions;
using iot.Application.Common.Security.JwtBearer;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Services.Authenticateion;

public class IdentityService : IIdentityService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IJwtService _jwtService;

    public IdentityService(IUnitOfWorks unitOfWorks, IJwtService jwtService)
    {
        _unitOfWorks = unitOfWorks;
        _jwtService = jwtService;
	}

    #endregion

	public async Task<(AccessToken Token,string Message)> SignInUserAsync(string phoneNumber,string password,CancellationToken cancellationToken)
	{
        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(phoneNumber, cancellationToken);
        var status = user.GetUserSignInStatusResultWithMessage(password);
        string message = string.Empty;

        if (status.Status.IsSucceeded())
        {
            AccessToken token = await _jwtService.GenerateAccessToken(user, cancellationToken);
            if (token.Token is null)
                message = "user is not authenticated !";
            else
                message = status.message;

            return (token, message);
        }

        return (new AccessToken(), status.message);
    }
}
