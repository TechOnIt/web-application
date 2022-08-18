using Microsoft.Extensions.Logging;

namespace iot.Application.Commands.Users.CreateUser;

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

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create new user instance.
        var newUser = User.CreateNewInstance(request.Email, request.PhoneNumber);

        // Set password hash.
        newUser.SetPassword(PasswordHash.Parse(request.Password));

        // Set full name.
        newUser.SetFullName(new FullName(request.Name, request.Surname));

        var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

        try
        {
            // Add new to database.
            bool wasSaved = await _unitOfWorks.SqlRepository<User>().AddAsync(newUser, saveNow: true, cancellationToken);
            await transAction.CommitAsync();

            if (wasSaved)
            {
                // TODO:
                // Log error.
            }
        }
        catch(Exception exp)
        {
            await transAction.RollbackAsync();
            _logger.Log(LogLevel.Critical,exp.Message);
        }

        return Result.Ok(newUser.Id);
    }
    #endregion
}

