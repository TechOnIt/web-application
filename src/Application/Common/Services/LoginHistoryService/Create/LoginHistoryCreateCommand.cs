using FluentValidation;
using iot.Application.Models;
using MediatR;

namespace iot.Application.Common.Services.LoginHistoryService.Create;

public class LoginHistoryCreateCommand : IRequest
{
    public string Ip { get; set; }
}

public class LoginHistoryCreateCommandValidator : BaseFluentValidator<LoginHistoryCreateCommand>
{
    public LoginHistoryCreateCommandValidator()
    {
        When(loginHistory => string.IsNullOrEmpty(loginHistory.Ip), () =>
        {
            RuleFor(loginHistory => loginHistory.Ip)
                .NotEmpty()
                .WithMessage("آی پی دستگاه اجباری است");
        });
    }
}