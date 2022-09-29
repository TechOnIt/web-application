using iot.Application.Common.Security.JwtBearer;
using iot.Application.Common.ViewModels.Structures.Authentication;
using iot.Domain.Entities.Product;
using iot.Infrastructure.Persistence.Context.Identity;
using System.Security.Claims;

namespace iot.Application.Repositories.SQL.Structures;

internal sealed class StructureRepository : IStructureRepository
{
    #region DI & Ctor
    private readonly IIdentityContext _context;
    private readonly IJwtService _jwtService;

    public StructureRepository(IIdentityContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }


    #endregion

    public async Task<StructureAccessToken?> GenerateAccessToken(Structure structure)
    {
        #region Validation
        if (structure.Id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(structure.Id));
        }
        if (string.IsNullOrEmpty(structure.Name))
        {
            throw new ArgumentNullException(nameof(structure.Name));
        }
        #endregion

        var accessToken = new StructureAccessToken();

        // Add identity claims.
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, structure.Id.ToString())
        };

        #region Refresh Token
        // Refresh token expire date time.
        var refreshTokenExpireAt = DateTime.Now.AddHours(3);
        accessToken.RefreshTokenExpireAt = refreshTokenExpireAt.ToString("yyyy/MM/dd HH:mm:ss");
        // Generate refresh token.
        accessToken.RefreshToken = _jwtService.GenerateTokenWithClaims(claims);
        #endregion

        #region Token
        claims.Add(new Claim(ClaimTypes.Name, structure.Name));

        // Token expire date time.
        var tokenExpiredAt = DateTime.Now.AddMinutes(5);
        accessToken.TokenExpireAt = tokenExpiredAt.ToString("yyyy/MM/dd HH:mm:ss");
        // Generate token.
        accessToken.Token = _jwtService.GenerateTokenWithClaims(claims,
            tokenExpiredAt);
        #endregion

        return accessToken;
    }
}