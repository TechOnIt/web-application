using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.Management.BanUser;

public class BanUserCommand : IRequest<Result>, ICommittableRequest
{
    public string? Id { get; set; }
}

public class BanUserCommandHandler : IRequestHandler<BanUserCommand, Result>
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

    public async Task<Result> Handle(BanUserCommand request, CancellationToken cancellationToken)
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
            // ban user & save.
            user.SetIsBaned(true);
            bool saveWasSuccess = await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, saveNow: true, cancellationToken);
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
            _logger.Log(LogLevel.Critical, exp.Message);
            await transAction.RollbackAsync();
        }

        return Result.Ok();
    }
}