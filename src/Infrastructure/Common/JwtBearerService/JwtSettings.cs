namespace TechOnIt.Infrastructure.Common.JwtBearerService;

public class JwtSettings
{
    public const string SecretKey = "235sgfb43534fbdh";
    public const string EncrypKey = "sgasgrhreh543643";
    public const string Issuer = "";
    public const string Audience = "";
    public const int NotBeforeMinutes = 0;
    public int ExpirationMinutes = 20;

    public string SecretKeyP { get; set; } = "235sgfb43534fbdh";
    public string EncrypKeyP { get; set; } = "sgasgrhreh543643";
    public string AudienceP { get; set; } = "";
    public string IssuerP { get; set; } = "";
}