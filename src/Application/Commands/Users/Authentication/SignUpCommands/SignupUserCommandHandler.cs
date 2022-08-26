using Mapster;

namespace iot.Application.Commands.Users.Authentication.SignUpCommands;

internal sealed class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, Result>
{
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public SignupUserCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    public async Task<Result> Handle(SignupUserCommand request, CancellationToken cancellationToken)
    {
        // Check username is unique?
        bool userIsExist = await _unitOfWorks.SqlRepository<User>()
            .IsExistsAsync(u => u.Username == request.PhoneNumber);

        if (userIsExist)
            return Result.Fail("duplicate username");
        else
        {
            await _unitOfWorks.SqlRepository<User>().AddAsync(request.Adapt<User>());
            await _mediator.Publish(new SignUpUserNotification());
        }

        return Result.Ok();
    }
}

internal sealed class SignupUserCommandValidator : BaseFluentValidator<SignupUserCommand>
{
    public SignupUserCommandValidator()
    {
        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .Length(11)
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            // TODO:
            // Uncomment this for strong password! :)
            .Matches(RegexConstant.Password)
            .MinimumLength(IdentitySettingConstant.MinimumPasswordLength)
            .MaximumLength(IdentitySettingConstant.MaximumPasswordLength)
            ;

        RuleFor(u => u.RepeatPassword)
            .NotEmpty()
            .Equal(u => u.Password)
            ;
    }
}
