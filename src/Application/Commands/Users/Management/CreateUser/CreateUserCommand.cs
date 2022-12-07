using TechOnIt.Application.Common.Models.ViewModels.Users;
using Microsoft.Extensions.Logging;
using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Users.Management.CreateUser;

public class CreateUserCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public string PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
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
        var userViewModel = new UserViewModel(request.PhoneNumber, request.PhoneNumber, request.Password,
            request.Name, request.Surname, request.Email);

        try
        {
            var result = await _userService.CreateUserAsync(userViewModel, cancellationToken);
            if (result.Status.IsDuplicate())
                return Result.Fail("user with this phonenumber has already been registered in the system");
            else if (result.Status.IsFailed())
                return Result.Fail("An error occurred");

            // TODO:
            // Refactor this response.
            if (!result.UserId.HasValue)
                return Result.Ok();

            return Result.Ok(result.UserId.Value);
        }
        catch (AppException exp)
        {
            throw new CommandException(IdentityCrudStatus.ServerError, $"{DateTime.Now}", exp, null);
        }
    }
}