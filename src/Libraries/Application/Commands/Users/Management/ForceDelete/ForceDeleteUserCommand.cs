namespace TechOnIt.Application.Commands.Users.Management.ForceDelete;

public class ForceDeleteUserCommand : IRequest<object>, ICommittableRequest
{
    public string Id { get; set; }
}

public class ForceDeleteUserCommandHandler : IRequestHandler<ForceDeleteUserCommand, object>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public ForceDeleteUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<object> Handle(ForceDeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = Guid.Parse(request.Id);

            var user = await _unitOfWorks.UserRepository.FindByIdAsync(userId, cancellationToken);
            if (user is null)
                return ResultExtention.NotFound("User was not found!");

            Task deleteUser = _unitOfWorks.UserRepository.DeleteByIdAsync(userId, cancellationToken);
            await deleteUser;

            if (deleteUser.IsCompleted)
                return ResultExtention.BooleanResult(true);
            else
                return ResultExtention.BooleanResult(false);
        }
        catch (Exception exp)
        {
            return ResultExtention.Failed($"Error Ocurred : {exp.Message}");
        }
    }
}