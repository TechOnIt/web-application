using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using iot.Domain.ValueObjects;
using MediatR;

namespace iot.Application.Commands.Users.Management;

public class CreateUserCommand : Command<Result<Guid>>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, Result<Guid>>
{
    #region DI & Ctor
    public IUserRepository _userRepository { get; set; }

    public CreateUserCommandHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }
    #endregion

    protected override async Task<Result<Guid>> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Create new user instance.
        var newUser = User.CreateNewInstance(request.Email, request.PhoneNumber);

        // Set password hash.
        newUser.Password = PasswordHash.Parse(request.Password);

        // Set full name.
        newUser.FullName = new FullName(request.Name, request.Surname);

        // Add new to database.
        bool wasSaved = await _userRepository.AddAsync(newUser, saveNow: true, cancellationToken);
        if (wasSaved)
        {
            // TODO:
            // Log error.
        }

        return Result.Ok(newUser.Id);
    }
}

public class CreateUserCommandValidator : BaseFluentValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("name can not be empty !")
            ;

        RuleFor(u => u.Surname)
            .NotEmpty()
            .WithMessage("Surname can not be empty !")
            ;

        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .Matches(RegexConstant.Email)
            .WithMessage("Email is required !")
            ;

        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .Length(11)
            .WithMessage("PhoneNumber must be 11 character.")
            .NotNull()
            .WithMessage("PhoneNumber is required !")
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password can not be empty!")
            ;
    }
}