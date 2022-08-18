
namespace iot.Application.Commands.Users.CreateUser;

public class CreateUserValidation : BaseFluentValidator<CreateUserCommand>
{
    public CreateUserValidation()
    {
        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .Length(11)
            .NotNull()
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(300)
            ;

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)
            .MaximumLength(200)
            .Matches(RegexConstant.Email)
            ;

        RuleFor(u => u.Name)
            .MaximumLength(50)
            ;

        RuleFor(u => u.Surname)
            .MaximumLength(50)
            .NotEmpty()
            ;
    }
}

