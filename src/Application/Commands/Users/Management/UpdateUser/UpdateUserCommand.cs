namespace iot.Application.Commands.Users.Management.UpdateUser;

public class UpdateUserCommand : IRequest<Result<string>>, ICommittableRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? ConcurrencyStamp { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public UpdateUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();
        // map id to guid instance.
        var userId = Guid.Parse(request.Id);

        // map concurrency stamp to instance.
        var concurrencyStamp = Concurrency.Parse(request.ConcurrencyStamp);

        // find user by id.
        var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);

        try
        {
            // user not found?
            if (user == null)
                return Result.Fail("User was not found!");

            ;    // concurrency stamp was not match?
            if (user.ConcurrencyStamp != concurrencyStamp)
                return Result.Fail("User was edited passed times, get latest user info.");

            // map user detail's.
            user.SetEmail(request.Email);
            user.SetFullName(new FullName(request.Name, request.Surname));
            // create new concurrency stamp.
            // TODO:
            // change stamp automaticly.
            user.RefreshConcurrencyStamp();

            bool wasSaved = await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, true, cancellationToken);
            if (wasSaved == false)
            {
                // TODO:
                // Add error log.
                return Result.Fail("UnAhead Error Happent .");
            }
            await transAction.CommitAsync();
        }
        catch
        {
            await transAction.RollbackAsync();
            Result.Fail("an Error ocured .");
        }

        // find user by id.
        return Result.Ok(user.ConcurrencyStamp.ToString());
    }
}
