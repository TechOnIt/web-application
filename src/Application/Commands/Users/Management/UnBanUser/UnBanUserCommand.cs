using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}

public class UnBanUserCommandHandler : IRequestHandler<UnBanUserCommand, Result>
{
    #region Cosntructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly ILogger<UnBanUserCommandHandler> _logger;

    public UnBanUserCommandHandler(IUnitOfWorks unitOfWorks, ILogger<UnBanUserCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }

    #endregion

    public async Task<Result> Handle(UnBanUserCommand request, CancellationToken cancellationToken)
    {
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);

        // find user by id.
        var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);
        if (user == null)
            return Result.Fail("User was not found!");

        // TODO:
        // Move transaction to pipeline...
        var trasnAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

        try
        {
            // ban user & save.
            user.SetIsBaned(false);
            await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, cancellationToken);

            await trasnAction.CommitAsync();
        }
        catch (Exception exp)
        {
            await trasnAction.RollbackAsync();
            _logger.Log(LogLevel.Critical, exp.Message);
        }

        return Result.Ok();
    }
}