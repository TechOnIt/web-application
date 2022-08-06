using iot.Application.Repositories.SQL.Users;


namespace iot.Application.Commands.Users.Management;

public class SetPasswordUserCommand : Command<Result>
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}

public class SetPasswordUserCommandHandler : CommandHandler<SetPasswordUserCommand, Result>
{
    #region DI & Ctor
    private readonly IUserRepository _userRepository;

    public SetPasswordUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    #endregion

    protected override async Task<Result> HandleAsync(SetPasswordUserCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);
        // find user by id.
        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
        if (user == null)
            return Result.Fail("User was not found!");

        // Set new password.
        user.Password = PasswordHash.Parse(request.Password);

        // Update user.
        bool saveWasSuccess = await _userRepository.UpdateAsync(user, saveNow: true, cancellationToken);
        if (saveWasSuccess == false)
        {
            // TODO:
            // add error log.
            return Result.Fail("An error was occured. try again later.");
        }
        return Result.Ok();
    }
}

public class SetPasswordUserCommandValidator : BaseFluentValidator<SetPasswordUserCommand>
{
    public SetPasswordUserCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(300)
            ;

        RuleFor(user => user.RepeatPassword)
            .Equal(user => user.Password)
            ;
    }
}