using iot.Application.Common.Interfaces;
using iot.Application.Repositories.UnitOfWorks.Identity;

namespace iot.Application.Commands.Users.Management.SetUserPassword;

public class SetUserPasswordCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}

public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, Result>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public SetUserPasswordCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion


    public async Task<Result> Handle(SetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        // map id to guid i;nstance.
        var userId = Guid.Parse(request.Id);

        // find user by id.
        var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);


        if (user == null)
            return Result.Fail("User was not found!");

        // Set new password.
        user.SetPassword(PasswordHash.Parse(request.Password));

        // TODO:
        // Move transaction to pipeline...
        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();
        try
        {
            // Update user.
            await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, cancellationToken);

            // TODO:
            // Move transaction to pipeline...
            await transAction.CommitAsync();
        }
        catch
        {
            // TODO:
            // Move transaction to pipeline...
            await transAction.RollbackAsync();
            return Result.Fail("An error was occured. try again later.");
        }

        return Result.Ok();
    }
}