namespace TechOnIt.Application.Commands.Users.Management.BanUser;

public class BanUserValidation : BaseFluentValidator<BanUserCommand>
{
    public BanUserValidation()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotNull()
            ;
    }
}