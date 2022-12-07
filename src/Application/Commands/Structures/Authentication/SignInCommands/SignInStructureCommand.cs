using iot.Application.Common.Models.ViewModels.Structures.Authentication;

namespace iot.Application.Commands.Structures.Authentication.SignInCommands;

public class SignInStructureCommand : IRequest<Result<StructureAccessToken>>
{
    public string ApiKey { get; set; }
    public string Password { get; set; }
}

public sealed class SignInStructureCommandValidator : BaseFluentValidator<SignInStructureCommand>
{
    public SignInStructureCommandValidator()
    {
        RuleFor(u => u.ApiKey)
            .NotEmpty()
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            // TODO:
            // Uncomment this for strong password!
            //.Matches(RegexConstant.Password)
            ;
    }
}