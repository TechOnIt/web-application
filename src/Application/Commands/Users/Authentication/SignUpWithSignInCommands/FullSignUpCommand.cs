using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using Microsoft.AspNetCore.Http;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;

namespace TechOnIt.Application.Commands.Users.Authentication.SignUpWithSignInCommands;

public class FullSignUpCommand : IRequest<object>, ICommittableRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsPersistent { get; set; }
}

public class FullSignUpCommandHandler : IRequestHandler<FullSignUpCommand, object>
{
    #region constructor
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;
    public FullSignUpCommandHandler(IIdentityService identityService, IUnitOfWorks unitOfWorks, IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        _identityService = identityService;
        _unitOfWorks = unitOfWorks;
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(FullSignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User newUser = new User(request.Username);
            newUser.SetPassword(new PasswordHash(request.Password));

            var singUpResult = await _identityService.RegularSingUpAsync(newUser, cancellationToken);
            if (singUpResult is object)
            {
                var user = await _unitOfWorks.UserRepository.FindByIdAsNoTrackingAsync(newUser.Id, cancellationToken);
                if (user == null) return ResultExtention.BooleanResult(false);

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                };

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
                    new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), authProperties);

                await _mediator.Publish(new SignInUserNotifications());

                return ResultExtention.BooleanResult(true);
            }

            return ResultExtention.BooleanResult(false);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}