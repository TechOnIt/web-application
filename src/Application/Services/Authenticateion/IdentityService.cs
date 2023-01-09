using Microsoft.Extensions.Caching.Distributed;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Models.DTOs.Users.Authentication;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Application.Common.Security.JwtBearer;
using TechOnIt.Application.Services.AssemblyServices;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Infrastructure.Common.Extentions;
using TechOnIt.Infrastructure.Common.Notifications.KaveNegarSms;
using TechOnIt.Infrastructure.Common.Notifications.Results;

namespace TechOnIt.Application.Services.Authenticateion;

public class IdentityService : IIdentityService
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IDistributedCache _distributedCache;
    private readonly IKaveNegarSmsService _kavenegarAuthService;
    private readonly IJwtService _jwtService;
    private readonly IAppSettingsService<AppSettingDto> _appSetting;

    public IdentityService(IUnitOfWorks unitOfWorks,
        IKaveNegarSmsService kavenegarAuthService,
        IDistributedCache distributedCache,
        IJwtService jwtService,
        IAppSettingsService<AppSettingDto> appSetting)
    {
        _unitOfWorks = unitOfWorks;
        _kavenegarAuthService = kavenegarAuthService;
        _distributedCache = distributedCache;
        _jwtService = jwtService;
        _appSetting = appSetting;
    }
    #endregion

    #region Sign-In
    /// <summary>
    /// Generate otp and send sms for user phone number.
    /// </summary>
    /// <param name="phoneNumber">User phone number (identity).</param>
    /// <returns>If there is a problem, the code will return null.</returns>
    public async Task<(string? Code, string Message)> SendOtpAsync(string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        // Find user by phone number.
        var user = await _unitOfWorks.UserRepository.FindByPhoneNumberWithRolesNoTrackingAsync(phoneNumber, cancellationToken);
        if (user is null)
            return (null, $"cant find user with phonenumber: {phoneNumber}");

        // Read otp code from cache.
        var newOtpCode = await _distributedCache.GetStringAsync(IdentitySettingConstant.OtpCodeKey, cancellationToken);

        // Was the code already generated?
        if (newOtpCode is not null)
            return (null, "Code was already generated.");

        // Create result and set fail default.
        (SendStatus Status, string Message) sendResult = (SendStatus.BadRequest, string.Empty);

        // If cache is null or expired.
        // Generate new OTP code.
        newOtpCode = user.GenerateNewOtpCode();

        // Validate generated otp code.
        if (newOtpCode is null)
        {
            // TODO:
            // Log here!
            return (null, "An error was occured!");
        }

#if DEBUG
        Console.WriteLine($"Otp Code is {newOtpCode}");
#endif
#if !DEBUG

        // Send otp as template (Lookup).
        //var sendResult = await _kavenegarAuthService.SendAuthSmsAsync(phoneNumber, "", "", user.OtpCode.ToString());

        // Send otp as a normal sms.
        sendResult = await _kavenegarAuthService.SendAsync(phoneNumber, $"Techonit\nYour verify code:\n{newOtpCode}");
        // Check send sms status result.
        if (!sendResult.Status.IsSendSuccessfully())
            return (null, sendResult.Message);
#endif

        // Set OTP code to cache with "otp-code" key.
        // Expiration setting up to 2 minutes.
        await _distributedCache.SetStringAsync(IdentitySettingConstant.OtpCodeKey, newOtpCode.Encrypt(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(IdentitySettingConstant.OtpExpirationDurationPerMinute)
            },
            cancellationToken);


        return (newOtpCode, $"OTP has been sent.");
    }

    public async Task<(AccessToken? Token, string Message)> VerifySignInOtpAsync(string otpCode, string phonenumber,
        CancellationToken cancellationToken = default)
    {
        // Find user by phone number.
        var user = await _unitOfWorks.UserRepository.FindByPhoneNumberWithRolesNoTrackingAsync(phonenumber, cancellationToken);
        // Null handling Validation.
        if (user is null)
            return (new AccessToken(), $"Can't find user with {phonenumber} phonenumber!");

        // Read otp code from cache.
        var EncryptedOtpCodeCache = await _distributedCache.GetStringAsync(IdentitySettingConstant.OtpCodeKey, cancellationToken);        // Validation
        // Null handling.
        if (EncryptedOtpCodeCache is null)
            return (new AccessToken(), "The code has expired!");
        // Decrypt otp code.
        var otpCodeCache = EncryptedOtpCodeCache.Decrypt();
        // Compare code.
        if (otpCodeCache != otpCode)
            return (new AccessToken(), "input otp code is not valid !");

        if (user.ConfirmedPhoneNumber == false)
            return (new AccessToken(), $"phonenumber is not confirmed yet !");

        var getUserClaims = await user.GetClaims();
        AccessToken? token = await GetUserAccessToken(user, cancellationToken);
        return (token, "welcome !");
    }

    public async Task<(AccessToken? Token, SigInStatus Status)?> SignInUserAsync(string username, string password,
        CancellationToken cancellationToken = default)
    {
        AccessToken? accessToken = new();

        var user = await _unitOfWorks.UserRepository.FindByUsernameWithRolesNoTrackingAsync(username, cancellationToken);
        if (user is null)
            return (accessToken, SigInStatus.NotFound);

        var status = user.GetUserSignInStatusResultWithMessage(password);

        if (!status.Status.IsSucceeded())
            return (accessToken, SigInStatus.WrongInformations);

        accessToken = await GetUserAccessToken(user, cancellationToken);
        if (accessToken is null)
            status.message = "user is not authenticated";

        return (accessToken, SigInStatus.Succeeded);
    }
    #endregion

    #region Sign-Up
    public async Task<(string? Code, SigInStatus Status)> SignUpAndSendOtpCode(CreateUserDto user,
        CancellationToken cancellationToken = default)
    {
        bool canRegister = await _unitOfWorks.UserRepository.IsExistsByPhoneNumberAsync(user.PhoneNumber);
        if (!canRegister) return (null, SigInStatus.DuplicateUser);

        var newUser = user.Adapt<User>();

        await _unitOfWorks.UserRepository.CreateAsync(newUser, cancellationToken);
        newUser.GenerateNewOtpCode();
        await _unitOfWorks.SaveAsync();

        if (newUser is null)
            return (null, SigInStatus.Error);

        var sendOtpresult = await _kavenegarAuthService.SendAuthSmsAsync(user.PhoneNumber, "", "", "1234 Test");

        if (!sendOtpresult.Status.IsSendSuccessfully())
            return (null, SigInStatus.SmsError);

        return ("1234!", SigInStatus.Succeeded);
    }

    public async Task<(AccessToken? Token, string Message)> SignUpWithOtpAsync(string phonenumber, string otpCode,
        CancellationToken cancellationToken = default)
    {
        var cachedOtpCode = await _distributedCache.GetStringAsync(IdentitySettingConstant.OtpCodeKey, cancellationToken);
        if (cachedOtpCode is null)
            return (new AccessToken(), $"Token is expired.");

        var user = await _unitOfWorks.UserRepository.FindByPhoneNumberWithRolesNoTrackingAsync(phonenumber, cancellationToken);
        if (user is null)
            return (new AccessToken(), $"can not find user with phonenumber : {phonenumber}");

        if (cachedOtpCode != otpCode)
            return (new AccessToken(), "otp code is not valid");

        user.ConfirmPhoneNumber();

        await _unitOfWorks.UserRepository.UpdateAsync(user,cancellationToken);
        await _unitOfWorks.SaveAsync();

        AccessToken? accessToken = await GetUserAccessToken(user, cancellationToken);

        if (accessToken is null)
            return (accessToken, "error !");

        return (accessToken, "welcome !");
    }
    #endregion

    #region Privates
    private async Task<AccessToken?> GetUserAccessToken(User user, CancellationToken cancellationToken)
    {
        AccessToken accessToken = new();
        try
        {
            #region Token
            var getUserClaimsWithRoles = await user.GetClaims();
            DateTime tokenExpiresAt = DateTime.Now.AddMinutes(5);
#if DEBUG
            // In debug mode token life is longer.
            tokenExpiresAt = tokenExpiresAt.AddMinutes(15);
#endif
            // Generate token with expiration date & time.
            accessToken.Token = await _jwtService.GenerateTokenAsync(claims: getUserClaimsWithRoles,
                expiresAt: tokenExpiresAt, cancellationToken);
            accessToken.TokenExpireAt = tokenExpiresAt.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion

            #region Refresh Token
            var getUserClaims = await user.GetIdAsClaim();
            DateTime refreshTokenExpiresAt = DateTime.Now.AddDays(3);
#if DEBUG
            // In debug mode token life is longer.
            refreshTokenExpiresAt = refreshTokenExpiresAt.AddDays(2);
#endif
            // Generate refresh token with expiration date & time.
            accessToken.RefreshToken = await _jwtService.GenerateTokenAsync(claims: getUserClaims,
                expiresAt: refreshTokenExpiresAt, cancellationToken);
            accessToken.RefreshTokenExpireAt = refreshTokenExpiresAt.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion
        }
        catch
        {
            // TODO:
            // Log error!
        }
        return accessToken;
    }
    #endregion
}