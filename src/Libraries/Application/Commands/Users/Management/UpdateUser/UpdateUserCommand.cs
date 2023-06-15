namespace TechOnIt.Application.Commands.Users.Management.UpdateUser;

public class UpdateUserCommand : IRequest<object>, ICommittableRequest
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string ConcurrencyStamp { get; set; }
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

        try
        {
            var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return ResultExtention.NotFound("User was not found!");
            if (user.IsConcurrencyStampValidate(request.ConcurrencyStamp))
                return ResultExtention.Failed("User was edited passed times, get latest user info.");
            user.SetEmail(request.Email);
            user.SetFullName(new FullName(request.Name, request.Surname));
            await _unitOfWorks.UserRepository.UpdateAsync(user, cancellationToken);
            return user.ConcurrencyStamp.ToString();
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}