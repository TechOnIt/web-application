using iot.Application.Common.Interfaces;

namespace iot.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}