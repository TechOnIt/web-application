using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Application.Services.AssemblyServices;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public class JwtService : IJwtService
{
    #region constructure
    private readonly IAppSettingsService<AppSettingDto> _appSetting;
    public JwtService(IAppSettingsService<AppSettingDto> appSetting)
    {
        _appSetting = appSetting;
    }
    #endregion

    public async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims, DateTime expiresAt, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.FromResult(Generate(claims, expiresAt));
    }

    private string Generate(IEnumerable<Claim> claims, DateTime? expiresAt = null)
    {
        var secretKey = Encoding.UTF8.GetBytes(_appSetting.Value.JwtSettings.SecretKey); // it must be atleast 16 characters or more
        var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _appSetting.Value.JwtSettings.Issuer,
            Audience = _appSetting.Value.JwtSettings.Audience,
            IssuedAt = DateTime.Now.ToUniversalTime(),
            NotBefore = DateTime.Now.ToUniversalTime(),
            SigningCredentials = signinCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        #region Token expiration
        if (expiresAt.HasValue)
        {
            descriptor.Expires = expiresAt.Value.ToUniversalTime();
        }
        #endregion

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(descriptor);
        string token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}