using iot.Application.Common.Interfaces;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.Management.ForceDelete;

public class ForceDeleteUserCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}

public class ForceDeleteUserCommandHandler : IRequestHandler<ForceDeleteUserCommand, Result>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly ILogger<ForceDeleteUserCommandHandler> _logger;
    public ForceDeleteUserCommandHandler(IUnitOfWorks unitOfWorks, ILogger<ForceDeleteUserCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }

    #endregion

    public async Task<Result> Handle(ForceDeleteUserCommand request, CancellationToken cancellationToken = default)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);

        //var result = await _unitOfWorks.UserRepository

        // find user by id.
        var user = await _unitOfWorks.UserRepository.FindByIdAsync(userId, cancellationToken);
        if (user == null)
            return Result.Fail("User was not found!");

        await _unitOfWorks.UserRepository.DeleteByIdAsync(userId, cancellationToken);

        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

        try
        {
            // Delete user account.
            //await _unitOfWorks.SqlRepository<User>().DeleteAsync(user, cancellationToken);
            // TODO:
            // Move transaction to pipeline...
            await transAction.CommitAsync();

        }
        catch (Exception exp)
        {
            // TODO:
            // Move transaction to pipeline...
            await transAction.RollbackAsync();
            _logger.Log(LogLevel.Critical, exp.Message);
        }

        return Result.Ok();
    }
}