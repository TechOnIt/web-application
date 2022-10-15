namespace iot.Application.Common.DTOs.Users.Authentication;

public class AccessToken
{
    public string Token { get; set; }
    public string TokenExpireAt { get; set; }

    public string RefreshToken { get; set; }
    public string RefreshTokenExpireAt { get; set; }
}