using iot.Application.Repositories.SQL.Users;

namespace iot.Application.Commands.Users.Management;

public class RemoveUserAccounCommand : Command<Result>
{
    public string Id { get; set; }
}
public class RemoveUserAccounCommandHandler : CommandHandler<RemoveUserAccounCommand, Result>
{
    #region DI & Ctor
    private readonly IUserRepository _userRepository;

    public RemoveUserAccounCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    #endregion

    protected override async Task<Result> HandleAsync(RemoveUserAccounCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);
        // find user by id.
        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
        if (user == null)
            return Result.Fail("User was not found!");

        // delete user & save.
        user.IsDeleted = true;
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

public class RemoveUserAccounCommandValidator : BaseFluentValidator<RemoveUserAccounCommand>
{
    public RemoveUserAccounCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}