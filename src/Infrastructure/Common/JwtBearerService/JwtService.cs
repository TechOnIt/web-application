using System.Security.Claims;

namespace iot.Application.Services.JwtBearerService
{
    public class JwtService
    {
        /// <summary>
        /// Generate JWT Token with claims.
        /// </summary>
        /// <param name="expireDateTime">
        /// Expire date & time.
        /// </param>
        public void GenerateTokenWithClaims(List<Claim> claims, DateTime expireDateTime = default)
        {

        }
    }
}