using iot.Application.Common.Models;
using MediatR;

namespace iot.Application.Commands.LoginHistories;

public class LoginHistoryCreateCommand : Command<Result<Unit>>
{
    public string? Ip { get; set; }
}

public class LoginHistoryCreateCommandHandler : CommandHandler<LoginHistoryCreateCommand, Result<Unit>>
{
    #region DI & Ctor
    public LoginHistoryCreateCommandHandler(IMediator mediator)
        : base(mediator)
    {

    }
    #endregion
    protected override async Task<Result<Unit>> HandleAsync(LoginHistoryCreateCommand request, CancellationToken cancellationToken = default)
    {
        return Result.Ok(await Task.FromResult(Unit.Value));
    }
}

public class LoginHistoryCreateCommandValidator : BaseFluentValidator<LoginHistoryCreateCommand>
{
    public LoginHistoryCreateCommandValidator()
    {
        RuleFor(loginHistory => loginHistory.Ip)
                .NotEmpty()
                .WithMessage("آی پی دستگاه اجباری است")
                ;
    }
}