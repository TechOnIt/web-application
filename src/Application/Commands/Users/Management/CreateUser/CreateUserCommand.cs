using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Commands.Users.Management.CreateUser;

public class CreateUserCommand : IRequest<object>, ICommittableRequest
{
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordRepeat { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, object>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public CreateUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {

        try
        {
            var newUser = new User(request.Email, request.PhoneNumber);
            newUser.SetPassword(new PasswordHash(request.Password));
            newUser.SetFullName(new FullName(request.Name, request.Surname));

            var isDuplicate = await _unitOfWorks.UserRepository.IsExistsByPhoneNumberAsync(newUser.PhoneNumber);
            if (isDuplicate)
                return ResultExtention.Failed("user with this phonenumber has already been registered in the system");

            Task createUser = _unitOfWorks.UserRepository.CreateAsync(newUser, cancellationToken);
            await createUser;

            if (createUser.IsCompleted)
                return ResultExtention.IdResult(newUser.Id);
            else
                return ResultExtention.Failed("error ocurred at this moment - pls try again");
        }
        catch (Exception exp)
        {
            return ResultExtention.Failed($"error ocurred : {exp.Message}");
        }
    }
}