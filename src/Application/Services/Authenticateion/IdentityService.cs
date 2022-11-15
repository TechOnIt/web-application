using iot.Application.Common.Extentions;
using iot.Application.Common.Security.JwtBearer;
using iot.Application.Common.ViewModels.Users.Authentication;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;
using iot.Infrastructure.Common.Notifications.KaveNegarSms;
using Mapster;

namespace iot.Application.Services.Authenticateion;

public class IdentityService : IIdentityService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    //private readonly IJwtService _jwtService;
    private readonly IKaveNegarSmsService _kavenegarAuthService;

    public IdentityService(IUnitOfWorks unitOfWorks, /*IJwtService jwtService,*/ IKaveNegarSmsService kavenegarAuthService)
    {
        _unitOfWorks = unitOfWorks;
        //_jwtService = jwtService;
        _kavenegarAuthService = kavenegarAuthService;
    }

    #endregion

    #region login
    public async Task<(int Code, string Message)> SendOtpAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(phoneNumber, cancellationToken);
        if (user is null)
            return (0, $"cant find user with phonenumber : {phoneNumber}");

        if (user.ConfirmedPhoneNumber == false)
            return (0, "phoennumber is not confirmed !");

        if (user.OtpCode == 0)
        {
            user.NewOtpCode();
            await _unitOfWorks.SaveAsync();
        }

        var sendResult = await _kavenegarAuthService.SendAuthSmsAsync(phoneNumber, "", "", user.OtpCode.ToString());
        if (!sendResult.Status.SendSuccessfully())
            return (0, sendResult.Message);

        return (user.OtpCode, $"otp sent");
    }

    public async Task<(AccessToken Token, string Message)> SignInUserWithOtpAsync(int userInputOtp, string phonenumber, CancellationToken cancellationToken)
    {

        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(phonenumber, cancellationToken);
        if (user is null)
            return (new AccessToken(), $"cant find user with phonenumber : {phonenumber}");

        if (user.OtpCode != userInputOtp)
            return (new AccessToken(), "input otp code is not valid !");

        if (user.ConfirmedPhoneNumber == false)
            return (new AccessToken(), $"phonenumber is not confirmed yet !");

        AccessToken token = default;
        using (var _jwtService = new JwtService())
        {
            token = await _jwtService.GenerateAccessToken(user, cancellationToken);
            _jwtService.Dispose();
        }

        return (token, "welcome !");
    }

    public async Task<(AccessToken Token, string Message)> SignInUserAsync(string phoneNumber, string password, CancellationToken cancellationToken)
    {
        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(phoneNumber, cancellationToken);
        var status = user.GetUserSignInStatusResultWithMessage(password);
        string message = string.Empty;

        if (status.Status.IsSucceeded())
        {
            var _jwtService = new JwtService();

            AccessToken token = await _jwtService.GenerateAccessToken(user, cancellationToken);
            if (token.Token is null)
                message = "user is not authenticated !";
            else
                message = status.message;

            return (token, message);
        }

        return (new AccessToken(), status.message);
    }
    #endregion

    #region signUp
    public async Task<(int Code, string Message)> SignUpAndSendOtpCode(UserViewModel user, CancellationToken cancellationToken)
    {
        bool canRegister = await _unitOfWorks.UserRepository.IsExistsUserByPhoneNumberAsync(user.PhoneNumber);
        if (!canRegister) return (0, "user with this information is already exists in system");

        var newUser = await _unitOfWorks.UserRepository.CreateNewUser(user.Adapt<User>(), cancellationToken);
        newUser.NewOtpCode();
        await _unitOfWorks.SaveAsync();

        if (newUser is null)
            return (0, "error !");

        var sendOtpresult = await _kavenegarAuthService.SendAuthSmsAsync(user.PhoneNumber, "", "", newUser.OtpCode.ToString());

        if (!sendOtpresult.Status.SendSuccessfully())
            return (0, sendOtpresult.Message);

        return (newUser.OtpCode, $"{user.PhoneNumber}");
    }

    public async Task<(AccessToken Token, string Message)> SignUpWithOtpAsync(string phonenumber, int inputUserOtpcode, CancellationToken cancellationToken)
    {

        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(phonenumber, cancellationToken);
        if (user is null)
            return (new AccessToken(), $"can not find user with phonenumber : {phonenumber}");

        if (user.OtpCode != inputUserOtpcode)
            return (new AccessToken(), "otp code is not valid");

        user.ConfirmPhoneNumber();
        await _unitOfWorks.SqlRepository<User>().UpdateAsync(user);
        await _unitOfWorks.SaveAsync();

        var _jwtService = new JwtService();
        var token = await _jwtService.GenerateAccessToken(user, cancellationToken);

        if (token is null)
            return (new AccessToken(), "error !");

        return (token, "welcome !");
    }
    #endregion
}
