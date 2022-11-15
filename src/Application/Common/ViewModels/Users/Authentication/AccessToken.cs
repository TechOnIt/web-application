namespace iot.Application.Common.ViewModels.Users.Authentication;

public class AccessToken
{
    public string Token { get; set; }
    public string TokenExpireAt { get; set; }

    public string RefreshToken { get; set; }
    public string RefreshTokenExpireAt { get; set; }
}