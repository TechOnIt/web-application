using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Common.Enums.JwtServices;
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

    public async Task<(IEnumerable<Claim>? Claims, AccessTokenStatus Status)> GetTokenClaimsAsync(string token, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await Task.FromResult(GetTokenClaims(token));
        return result;
    }

    public async Task<(Claim? Claim, AccessTokenStatus Status)> FindInTokenClaimsAsync(string token, string ClaimType,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // Get all claims.
        var getClaims = await GetTokenClaimsAsync(token, cancellationToken);
        if(getClaims.Claims is null)
            return (null, getClaims.Status);

        // Find in claims.
        return (getClaims.Claims.FirstOrDefault(c => c.Type == ClaimType),
            getClaims.Status);
    }

    #region Private Methods
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

    private (IEnumerable<Claim>? Claims, AccessTokenStatus Status) GetTokenClaims(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSetting.Value.JwtSettings.SecretKey);
            //var encryptionkey = Encoding.UTF8.GetBytes(appSettings.EncryptionKey);
            var claimsPrincipal = handler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                },
                out SecurityToken securityToken);
            return (claimsPrincipal.Claims, AccessTokenStatus.Succeeded);
        }
        catch (SecurityTokenExpiredException)
        {
            // Token was expired.
            return (null, AccessTokenStatus.Expired);
        }
        catch(Exception)
        {
            // Another errors.
            return (null, AccessTokenStatus.Conflict);
        }
    }
    #endregion
}