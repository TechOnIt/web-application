namespace TechOnIt.Application.Commands.Users.Management.ResetUserPassword;

public class ResetUserPasswordCommand : IRequest<object>, ICommittableRequest
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}

public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, object>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public ResetUserPasswordCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO:
            // Refactor command result.
            var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return ResultExtention.NotFound("User was not found!");

            user.SetPassword(PasswordHash.Parse(request.Password));
            await _unitOfWorks.UserRepository.UpdateAsync(user, cancellationToken);
            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}