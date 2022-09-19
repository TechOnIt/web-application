using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Security.JwtBearer;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AccessToken>>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;

    public SignInUserCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator, IJwtService jwtService)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
        _jwtService = jwtService;
    }
    #endregion

    public async Task<Result<AccessToken>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        //var signinPassword = await _unitOfWorks.UserRepository.UserSignInByPasswordAsync(request.Username, request.Password, cancellationToken);

        //var user = await _context.Users
        //.Where(u => u.PhoneNumber == phoneNumber.Trim())
        //.Include(u => u.UserRoles)
        //.ThenInclude(ur => ur.Role)
        //.AsNoTracking()
        //.FirstOrDefaultAsync(cancellationToken);

        //string message = string.Empty;

        //if (user == null)
        //    message = "Not found user !";
        //else if (user.IsBaned is true)
        //    message = "Username or password is wrong!";
        //else if (user.LockOutDateTime != null)
        //    message = "user is locked !";
        //else if (user.Password != PasswordHash.Parse(password))
        //    message = "password is wrong!";

        //AccessToken token = await GenerateAccessToken(user, cancellationToken);
        //return Result.Ok(new AccessToken());

        //if (signinPassword.Token is null)
        //    return Result.Fail(signinPassword.Message);

        //await _mediator.Publish(new SignInUserNotifications()); // notification events
        //return Result.Ok(signinPassword.Token);

        return Result.Ok();
    }
}

public sealed class SignInUserCommandValidator : BaseFluentValidator<SignInUserCommand>
{
    public SignInUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(IdentitySettingConstant.MinimumUsernameLength)
            .MaximumLength(IdentitySettingConstant.MaximumUsernameLength)
            ;

        RuleFor(u => u.Password)
            .MinimumLength(IdentitySettingConstant.MinimumPasswordLength)
            .MaximumLength(IdentitySettingConstant.MaximumPasswordLength)
            // TODO:
            // Uncomment this for strong password!
            //.Matches(RegexConstant.Password)
            ;
    }
}