using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using iot.Domain.ValueObjects;

namespace iot.Application.Commands.Users.Management;

public class UpdateUserCommand : Command<Result<string>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string ConcurrencyStamp { get; set; }
}

public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand, Result<string>>
{
    #region DI & Ctor
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }
    #endregion

    protected override async Task<Result<string>> HandleAsync(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);
        // map concurrency stamp to instance.
        var concurrencyStamp = Concurrency.Parse(request.ConcurrencyStamp);
        // find user by id.
        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
        // user not found?
        if (user == null)
            return Result.Fail("User was not found!");
        // concurrency stamp was not match?
        if (user.ConcurrencyStamp != concurrencyStamp)
            return Result.Fail("User was edited passed times, get latest user info.");

        // map user detail's.
        user.SetEmail(request.Email);
        user.FullName = new FullName(request.Name, request.Surname);
        // create new concurrency stamp.
        // TODO:
        // change stamp automaticly.
        user.ConcurrencyStamp = Concurrency.NewToken();

        bool wasSaved = await _userRepository.UpdateAsync(user, true, cancellationToken);
        if (wasSaved == false)
        {
            // TODO:
            // Add error log.
            return Result.Fail("An error was occured. Try again later.");
        }
        // find user by id.
        return Result.Ok(user.ConcurrencyStamp.ToString());
    }
}

public class UpdateUserCommandValidator : BaseFluentValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .MaximumLength(200)
            .Matches(RegexConstant.Guid)
            ;

        RuleFor(u => u.Name)
            .MaximumLength(50)
            ;

        RuleFor(u => u.Surname)
            .MaximumLength(50)
            .NotEmpty()
            ;

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)
            .MaximumLength(200)
            .Matches(RegexConstant.Email)
            ;

        RuleFor(u => u.ConcurrencyStamp)
            .NotEmpty()
            .MaximumLength(200)
            ;
    }
}
