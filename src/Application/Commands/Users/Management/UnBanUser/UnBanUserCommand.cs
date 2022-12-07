using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}