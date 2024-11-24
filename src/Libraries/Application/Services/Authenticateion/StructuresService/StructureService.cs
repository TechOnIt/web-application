using TechOnIt.Application.Common.Models.ViewModels.Structures.Authentication;
using TechOnIt.Application.Common.Security.JwtBearer;
using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Application.Services.Authenticateion.StructuresService;

public class StructureService : IStructureService
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IJwtService _jwtService;
    private readonly ILogger<StructureService> _logger;
    public StructureService(IUnitOfWorks unitOfWorks,
        IJwtService jwtService,
        ILogger<StructureService> logger)
    {
        _jwtService = jwtService;
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }
    #endregion

    public async Task<StructureAccessToken> SignInAsync(string apiKey,
        string password, CancellationToken cancellationToken = default)
    {
        var structure = await _unitOfWorks.StructureRepository.FindByApiKeyAndPasswordAsync(Concurrency.Parse(apiKey), PasswordHash.Parse(password), cancellationToken);
        if (structure is null)
            throw new IdentityArgumentException("Username or password is wrong!");
        if(structure.IsActive == false)
            throw new StructureException("Structure is deactive.");


        StructureAccessToken accessToken = await GetStructureAccessToken(structure, cancellationToken);
        if (accessToken is null)
            throw new Exception("An error has occured.");

        return accessToken;
    }

    #region Privates
    private async Task<StructureAccessToken?> GetStructureAccessToken(StructureEntity structure, CancellationToken cancellationToken)
    {
        StructureAccessToken accessToken = new();
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
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return accessToken;
    }
    #endregion
}