namespace iot.Application.Common.Models.ViewModels.Structures.Authentication;

public class StructureAccessToken
{
    public string Token { get; set; }
    public string TokenExpireAt { get; set; }

    public string RefreshToken { get; set; }
    public string RefreshTokenExpireAt { get; set; }
}
