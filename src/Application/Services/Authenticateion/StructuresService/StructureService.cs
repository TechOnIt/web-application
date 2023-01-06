using TechOnIt.Application.Common.Models.ViewModels.Structures.Authentication;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Application.Common.Security.JwtBearer;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.Entities.Product.StructureAggregate;

namespace TechOnIt.Application.Services.Authenticateion.StructuresService;

public class StructureService : IStructureService
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IJwtService _jwtService;

    public StructureService(IUnitOfWorks unitOfWorks, IJwtService jwtService)
    {
        _jwtService = jwtService;
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<(StructureAccessToken? Token, string Message)?> SignInAsync(string apiKey, string password, CancellationToken cancellationToken = default)
    {
        StructureAccessToken? accessToken = new();
        var apiKeyObject = Concurrency.Parse(apiKey);
        var structure = await _unitOfWorks.StructureRepository.FindByApiKeyNoTrackingAsync(apiKeyObject, cancellationToken);
        if (structure is null)
            return (accessToken, "Structure not found.");
        var status = structure.GetStructureSignInStatusResultWithMessage(password);
        if (!status.Status.IsSucceeded())
            return (accessToken, status.message);

        accessToken = await GetStructureAccessToken(structure, cancellationToken);
        if (accessToken is null)
            status.message = "user is not authenticated";

        return (accessToken, status.message);

    }

    #region Privates
    private async Task<StructureAccessToken?> GetStructureAccessToken(Structure structure, CancellationToken cancellationToken)
    {
        StructureAccessToken? accessToken = new();
        try
        {
            #region Token
            var structuresAllClaims = await structure.GetClaims();
            DateTime tokenExpiresAt = DateTime.Now.AddMinutes(5);
#if DEBUG
            // In debug mode token life is longer.
            tokenExpiresAt = tokenExpiresAt.AddMinutes(30);
#endif
            // Generate token with expiration date & time.
            accessToken.Token = await _jwtService.GenerateTokenAsync(claims: structuresAllClaims,
                expiresAt: tokenExpiresAt, cancellationToken);
            accessToken.TokenExpireAt = tokenExpiresAt.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion

            #region Refresh Token
            var structureIdAsClaim = await structure.GetIdAsClaim();
            DateTime refreshTokenExpiresAt = DateTime.Now.AddDays(3);
#if DEBUG
            // In debug mode token life is longer.
            refreshTokenExpiresAt = refreshTokenExpiresAt.AddDays(2);
#endif
            // Generate refresh token with expiration date & time.
            accessToken.RefreshToken = await _jwtService.GenerateTokenAsync(claims: structureIdAsClaim,
                expiresAt: refreshTokenExpiresAt, cancellationToken);
            accessToken.RefreshTokenExpireAt = refreshTokenExpiresAt.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion
        }
        catch
        {
            // TODO:
            // Log error!
        }
        return accessToken;
    }
    #endregion
}