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
}