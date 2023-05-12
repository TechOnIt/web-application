namespace TechOnIt.Application.Commands.Users.Management.UpdateUser;

public class UpdateUserCommandValidator : BaseFluentValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(u => u.Name)
            .MaximumLength(50);

        RuleFor(u => u.Surname)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(10)
            .MaximumLength(200)
            .Matches(RegexConstant.Email);

        RuleFor(u => u.RowVersion)
            .NotEmpty()
            .MaximumLength(200);
    }
}