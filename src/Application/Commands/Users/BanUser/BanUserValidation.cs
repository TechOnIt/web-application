namespace iot.Application.Commands.Users.BanUser;

public class BanUserValidation : BaseFluentValidator<BanUserCommand>
{
    public BanUserValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
    ;
    }
}
