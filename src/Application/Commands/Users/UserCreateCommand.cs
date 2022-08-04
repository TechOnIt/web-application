using FluentResults;
using FluentValidation;
using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using iot.Domain.Entities.Identity;
using iot.Domain.ValueObjects;

namespace iot.Application.Commands.Users;

public class UserCreateCommand : Command<Result<Guid>>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

public class UserCreateCommandHandler : CommandHandler<UserCreateCommand, Result<Guid>>
{
    #region DI & Ctor's
    public IUserRepository _userRepository { get; set; }

    public UserCreateCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion

    public async Task<Result<Guid>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
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

public class UserCreateCommandValidator : BaseFluentValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
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