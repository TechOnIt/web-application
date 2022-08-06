using iot.Application.Repositories.SQL.Users;

namespace iot.Application.Commands.Users.Management;

public class ForceDeleteUserCommand : Command<Result>
{
    public string Id { get; set; }
}
public class ForceDeleteUserCommandHandler : CommandHandler<ForceDeleteUserCommand, Result>
{
    #region DI & Ctor
    private readonly IUserRepository _userRepository;

    public ForceDeleteUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    #endregion

    protected override async Task<Result> HandleAsync(ForceDeleteUserCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);

        // find user by id.
        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
        if (user == null)
            return Result.Fail("User was not found!");

        // Delete user account.
        bool saveWasSuccess = await _userRepository.DeleteAsync(user, saveNow: true, cancellationToken);
        if (saveWasSuccess == false)
        {
            // TODO:
            // add error log.
            return Result.Fail("An error was occured. try again later.");
        }
        return Result.Ok();
    }
}

public class ForceDeleteUserCommandValidator : BaseFluentValidator<ForceDeleteUserCommand>
{
    public ForceDeleteUserCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}