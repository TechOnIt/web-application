using iot.Application.Repositories.SQL.Users;

namespace iot.Application.Commands.Users.Authentication;

public class SignInUserCommand : Command<Result>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class SignInUserCommandHandler : CommandHandler<SignInUserCommand, Result>
{
    #region DI & Ctor
    private readonly IUserRepository _userRepository;

    public SignInUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }
    #endregion

    protected override async Task<Result> HandleAsync(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = PasswordHash.Parse(request.Password);
        // Find user by username.
        var user = await _userRepository.FindByUsernameAsync(username: request.Username);
        if (user == null || user.Password != passwordHash)
            return Result.Fail("Username or password is wrong!");
        
        // TODO:
        // Generate access token for user.

        return Result.Ok();
    }
}

public class SignInUserCommandValidator : BaseFluentValidator<SignInUserCommand>
{
    public SignInUserCommandValidator()
    {
        // TODO:
        
    }
}