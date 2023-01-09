using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Users.Management.SetUserPassword;

public class SetUserPasswordCommand : IRequest<object>, ICommittableRequest
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}

public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, object>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public SetUserPasswordCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(SetUserPasswordCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return ResultExtention.NotFound("User was not found!");

            user.SetPassword(PasswordHash.Parse(request.Password));
            await _unitOfWorks.UserRepository.UpdateAsync(user,cancellationToken);
            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}