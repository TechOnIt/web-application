using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Security.JwtBearer;
using iot.Infrastructure.Persistence.Context.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace iot.Application.Repositories.SQL.Users;

internal sealed class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IIdentityContext _context;
    private readonly IJwtService _jwtService;

    public UserRepository(IIdentityContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    #endregion

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Username.ToLower().Trim() == username.ToLower().Trim(), cancellationToken);

    /// <summary>
    /// Find user by email or phone number with roles (AsNoTracking).
    /// </summary>
    /// <param name="identity">Email or Phone number</param>
    /// <returns>An specific user.</returns>
    public async Task<User?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default)
    => await _context.Users
        .Where(u => u.Email == identity.Trim().ToLower() || u.PhoneNumber == identity.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(stoppingToken);

    public async Task<IList<User>?> GetAllUsersByFilterAsync(Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter != null)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(filter);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
        }
    }

    /// <summary>
    /// this method is for signinuserwith password - it is like signinPassword in identityframwork
    /// we should design a result class for this situation
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Tuple</returns>
    public async Task<(string Message, AccessToken Token)> UserSignInByPasswordAsync(string phoneNumber,string password,CancellationToken cancellationToken=default)
    {
        // Find user by just Phone number with roles.
        var user = await _context.Users
        .Where(u => u.PhoneNumber == phoneNumber.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

        string message = string.Empty;

        if (user == null)
            message= "Not found user !";
        else if (user.IsBaned is true)
            message= "Username or password is wrong!";
        else if (user.LockOutDateTime != null)
            message= "user is locked !";
        else if (user.Password != PasswordHash.Parse(password))
            message= "password is wrong!";

        AccessToken token = await GenerateAccessToken(user, cancellationToken);
        return (message, token);
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
        accessToken.RefreshToken = _jwtService.GenerateTokenWithClaims(claims);
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
        accessToken.Token = _jwtService.GenerateTokenWithClaims(claims,
            tokenExpiredAt);
        #endregion

        return accessToken;
    }
}