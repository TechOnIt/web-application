using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Commands.Users.Authentication.SignInCookieCommands;

public class SignInCookieCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; }
}

public class SignInCookieCommandHandler : IRequestHandler<SignInCookieCommand, bool>
{
    #region Ctor
    private readonly IUserReports _userReports;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SignInCookieCommandHandler(IUserReports userReports,
        IHttpContextAccessor httpContextAccessor)
    {
        _userReports = userReports;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    public async Task<bool> Handle(SignInCookieCommand request, CancellationToken cancellationToken)
    {
        var user = await _userReports.FindByIdentityNoTrackAsync(request.Username, cancellationToken);
        // Check exist.
        if (user == null) return false;
        // Check password.
        if(user.Password != PasswordHash.Parse(request.Password)) return false;

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
        #region Roles
        //foreach (var userRole in user.UserRoles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        //}
        #endregion

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3),
            IsPersistent = request.IsPersistent,
            IssuedUtc = DateTimeOffset.UtcNow
        };

#if DEBUG
        authProperties.ExpiresUtc.Value.AddDays(4);
#endif

        cancellationToken.ThrowIfCancellationRequested();
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme)),
            authProperties);
        return true;
    }
}