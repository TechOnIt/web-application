namespace iot.Application.Commands.Users.Management.UpdateUser;

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
            .MinimumLength(10)
            .MaximumLength(200)
            .Matches(RegexConstant.Email)
            ;

        RuleFor(u => u.ConcurrencyStamp)
            .NotEmpty()
            .MaximumLength(200)
            ;
    }
}
