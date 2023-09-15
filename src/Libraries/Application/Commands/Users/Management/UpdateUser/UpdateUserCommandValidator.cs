namespace TechOnIt.Application.Commands.Users.Management.UpdateUser;

public class UpdateUserCommandValidator : BaseFluentValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotNull();

        When(u => !string.IsNullOrEmpty(u.Name) && u.Name.Length > 0, () =>
        {
            RuleFor(u => u.Name)
            .MinimumLength(3)
            .MaximumLength(50);
        });

        When(u => !string.IsNullOrEmpty(u.Surname) && u.Surname.Length > 0, () =>
        {
            RuleFor(u => u.Surname)
            .MinimumLength(3)
            .MaximumLength(50);
        });

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(10)
            .MaximumLength(200)
            .Matches(RegexConstant.Email);

        RuleFor(u => u.ConcurrencyStamp)
            .NotEmpty()
            .MaximumLength(200);
    }
}