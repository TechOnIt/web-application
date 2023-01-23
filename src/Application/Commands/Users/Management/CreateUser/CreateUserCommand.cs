using Microsoft.Extensions.Logging;
using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Commands.Users.Management.CreateUser;

public class CreateUserCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordRepeat { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    #region Constructor
    private readonly IUserService _userService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUserService userService, ILogger<CreateUserCommandHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    #endregion

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        // Create new user instance.
        var newUser = new User(request.Email, request.PhoneNumber);
        // Set user password
        if (!string.IsNullOrEmpty(request.Password))
            newUser.SetPassword(new PasswordHash(request.Password));
        // Set user fullname
        if (!string.IsNullOrEmpty(request.Name) || !string.IsNullOrEmpty(request.Surname))
            newUser.SetFullName(new FullName(request.Name, request.Surname));

        try
        {
            var createUserResult = await _userService.CreateUserAsync(newUser, cancellationToken);
            if (createUserResult.Status.IsDuplicate())
                return Result.Fail("user with this phonenumber has already been registered in the system");
            else if (createUserResult.Status.IsFailed())
                return Result.Fail("An error occurred");

            // TODO:
            // Refactor this response.
            if (!createUserResult.UserId.HasValue)
                return Result.Ok();

            return Result.Ok(createUserResult.UserId.Value);
        }
        catch (AppException exp)
        {
            throw new CommandException(IdentityCrudStatus.ServerError, $"{DateTime.Now}", exp, null);
        }
    }
}