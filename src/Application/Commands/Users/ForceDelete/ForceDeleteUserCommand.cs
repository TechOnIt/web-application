
using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.ForceDelete;

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

    public async Task<Result> Handle(ForceDeleteUserCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);

        // find user by id.
        var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);
        if (user == null)
            return Result.Fail("User was not found!");

        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

        try
        {
            // Delete user account.
            bool saveWasSuccess = await _unitOfWorks.SqlRepository<User>().DeleteAsync(user, saveNow: true, cancellationToken);
            await transAction.CommitAsync();

            if (saveWasSuccess == false)
            {
                // TODO:
                // add error log.
                return Result.Fail("An error was occured. try again later.");
            }
        }
        catch (Exception exp)
        {
            await transAction.RollbackAsync();
            _logger.Log(LogLevel.Critical,exp.Message);
        }

        return Result.Ok();
    }
}