using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Repositories.SQL.Users;

namespace iot.Application.Commands.Users.Authentication;

public sealed class SignInUserCommand : IRequest<Result<AccessToken>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AccessToken>>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public SignInUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<AccessToken>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        // Find user by Email or Phone number with roles.
        var user = await _unitOfWorks.UserRepository.FindByIdentityWithRolesAsync(identity: request.Username, cancellationToken);
        if (user == null || user.Password != PasswordHash.Parse(request.Password))
            return Result.Fail("Username or password is wrong!");

        // Generate access token.
        var accessToken = await _unitOfWorks.UserRepository.GenerateAccessToken(user);
        if (accessToken == null)
            return Result.Fail("An error has occured!");

        return Result.Ok(accessToken);
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