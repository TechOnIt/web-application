using iot.Application.Common.DTOs.Users.Authentication;
using iot.Infrastructure.Common.JwtBearerService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iot.Application.Common.Security.JwtBearer;

public class JwtService : IJwtService
{
    /// <summary>
    /// Generate JWT Token with claims.
    /// </summary>
    /// <param name="claims">List of claims in token payload.</param>
    /// <param name="expireDateTime">Expire date & time.</param>
    /// <returns>Access token.</returns>
    public string GenerateTokenWithClaims(List<Claim> claims, DateTime? expireDateTime = null)
    {
        var secretKey = Encoding.UTF8.GetBytes(JwtSettings.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256Signature);

        var encrytionKey = Encoding.UTF8.GetBytes(JwtSettings.EncrypKey);
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey),
            SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = JwtSettings.Issuer,
            Audience = JwtSettings.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(JwtSettings.NotBeforeMinutes),
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(claims),
            EncryptingCredentials = encryptingCredentials,
        };
        // Add expiration date time.
        if (expireDateTime != null)
            tokenDescriptor.Expires = expireDateTime;
        // Generate token.
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    /// <summary>
    /// Generate access token with Jwt Bearer.
    /// </summary>
    /// <param name="user">User instance with roles for generate access token.</param>
    /// <returns>Access token dto.</returns>
    public async Task<AccessToken> GenerateAccessToken(User user, CancellationToken stoppingToken = default)
    {
        var accessToken = new AccessToken();

        // Add identity claims.
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        #region Refresh Token
        // Refresh token expire date time.
        var refreshTokenExpireAt = DateTime.Now.AddHours(3);
        accessToken.RefreshTokenExpireAt = refreshTokenExpireAt.ToString("yyyy/MM/dd HH:mm:ss");
        // Generate refresh token.
        accessToken.RefreshToken = GenerateTokenWithClaims(claims);
        #endregion

        #region Token
        claims.Add(new Claim(ClaimTypes.Name, user.Username));

        // Add roles in claims.
        if (user.UserRoles != null || user.UserRoles?.Count > 0)
            foreach (var userRole in user.UserRoles)
                if (userRole.Role != null)
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name.ToString()));

        // Token expire date time.
        var tokenExpiredAt = DateTime.Now.AddMinutes(5);
        accessToken.TokenExpireAt = tokenExpiredAt.ToString("yyyy/MM/dd HH:mm:ss");
        // Generate token.
        accessToken.Token = GenerateTokenWithClaims(claims,
            tokenExpiredAt);
        #endregion

        return accessToken;
    }
}