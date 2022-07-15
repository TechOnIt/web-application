using iot.Domain.ValueObjects;
using MediatR;

namespace iot.Application.Common.Services.LoginHistory.Create;

public class LoginHistoryCreateCommand : IRequest
{
    public IPv4 Ip { get; set; }
}