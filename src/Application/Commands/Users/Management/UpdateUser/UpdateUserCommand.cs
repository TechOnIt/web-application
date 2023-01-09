using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Users.Management.UpdateUser;

public class UpdateUserCommand : IRequest<object>, ICommittableRequest
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? ConcurrencyStamp { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public UpdateUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
    {
        // map concurrency stamp to instance.
        var concurrencyStamp = Concurrency.Parse(request.ConcurrencyStamp);

        // find user by id.
        var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId, cancellationToken);

        try
        {
            // user not found?
            if (user == null)
                return ResultExtention.NotFound("User was not found!");

            ;    // concurrency stamp was not match?
            if (user.ConcurrencyStamp != concurrencyStamp)
                return ResultExtention.Failed("User was edited passed times, get latest user info.");

            // map user detail's.
            user.SetEmail(request.Email);
            user.SetFullName(new FullName(request.Name, request.Surname));

            // create new concurrency stamp.
            // TODO:
            // change stamp automaticly.
            user.RefreshConcurrencyStamp();
            await _unitOfWorks.UserRepository.UpdateAsync(user, cancellationToken);
            return user.ConcurrencyStamp.ToString();
        }
        catch(Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}
