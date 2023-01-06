﻿using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Common.Models.ViewModels.Structures.Authentication;

namespace TechOnIt.Application.Commands.Structures.Authentication.SignInCommands;

public class SignInStructureCommand : IRequest<object>
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