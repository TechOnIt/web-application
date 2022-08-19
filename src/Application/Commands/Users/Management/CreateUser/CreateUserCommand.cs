﻿using iot.Application.Repositories.UnitOfWorks.Identity;
using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.Management.CreateUser;

public class CreateUserCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    #region Constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUnitOfWorks unitOfWorks, ILogger<CreateUserCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _logger = logger;
    }
    #endregion

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create new user instance.
        var newUser = User.CreateNewInstance(request.Email, request.PhoneNumber);

        // Set password hash.
        newUser.SetPassword(PasswordHash.Parse(request.Password));

        // Set full name.
        newUser.SetFullName(new FullName(request.Name, request.Surname));

        // TODO:
        // Move transaction to pipeline...
        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

        try
        {
            // Add new to database.
            await _unitOfWorks.SqlRepository<User>().AddAsync(newUser, cancellationToken);
            await transAction.CommitAsync();
        }
        catch (Exception exp)
        {
            // TODO:
            // Move transaction to pipeline...
            await transAction.RollbackAsync();
            _logger.Log(LogLevel.Critical, exp.Message);
        }

        return Result.Ok(newUser.Id);
    }
}

