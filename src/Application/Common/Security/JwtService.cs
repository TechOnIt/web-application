using iot.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iot.Infrastructure.Common.JwtBearerService
{
    public class JwtService : IJwtService
    {
        /// <summary>
        /// Generate JWT Token with claims.
        /// </summary>
        /// <param name="expireDateTime">
        /// Expire date & time.
        /// </param>
        public string GenerateTokenWithClaims(List<Claim> claims,int expireDateTime=20)
        {
            var secretKey = Encoding.UTF8.GetBytes(JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes(JwtSettings.EncrypKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = JwtSettings.Issuer,
                Audience = JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(expireDateTime),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
                EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}