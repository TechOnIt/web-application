namespace iot.Application.Commands.Users.Management.ForceDelete;

public class ForceDeleteUserValidation : BaseFluentValidator<ForceDeleteUserCommand>
{
    public ForceDeleteUserValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}
