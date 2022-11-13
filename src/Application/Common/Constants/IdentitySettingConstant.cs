namespace iot.Application.Common.Constants;

public class IdentitySettingConstant
{
    #region Username
    public const int MinimumUsernameLength = 6;
    public const int MaximumUsernameLength = 50;
    #endregion

    #region Password
    public const int MinimumPasswordLength = 6;
    public const int MaximumPasswordLength = 100;
    #endregion

    #region Otp code
    public const string OtpCodeKey = "otp-code";
    public const int OtpExpirationDurationPerMinute = 2;
    #endregion
}