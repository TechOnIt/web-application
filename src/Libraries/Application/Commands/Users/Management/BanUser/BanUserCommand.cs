using Microsoft.Extensions.Logging;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Users.Management.BanUser;

public class BanUserCommand : IRequest<object>, ICommittableRequest
{
    public Guid UserId { get; set; }
}

public class BanUserCommandHandler : IRequestHandler<BanUserCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly ILogger<BanUserCommandHandler> _logger;

    public BanUserCommandHandler(IUnitOfWorks unitOfWorks, ILogger<BanUserCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }

    #endregion

    public async Task<object> Handle(BanUserCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId,cancellationToken);

            if (user == null)
                return ResultExtention.NotFound("User was not found!");

            user.Ban();
            var updateRes = _unitOfWorks.UserRepository.UpdateAsync(user,cancellationToken);
            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}