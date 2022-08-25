using iot.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}

