using MediatR;

namespace iot.Application.Common.Services.LoginHistoryService.Create;

public class LoginHistoryCreateCommandHandler : IRequestHandler<LoginHistoryCreateCommand>
{

    public Task<Unit> Handle(LoginHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}