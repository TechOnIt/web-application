using FluentValidation;
using iot.Application.Common.Models;
using MediatR;

namespace iot.Application.Commands.LoginHistories;

public class LoginHistoryCreateCommand : IRequest
{
    public string Ip { get; set; }
}

public class LoginHistoryCreateCommandHandler : IRequestHandler<LoginHistoryCreateCommand>
{

    public Task<Unit> Handle(LoginHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
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