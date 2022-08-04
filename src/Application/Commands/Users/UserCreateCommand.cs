using FluentValidation;
using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using iot.Domain.Entities.Identity;
using iot.Domain.ValueObjects;
using MediatR;

namespace iot.Application.Commands.Users
{
    public class UserCreateCommand : Command<Guid>
    {
        // why did i add ? to string type properties while strings are already null-able ?
        // the answer is here for compiler warnings when we define a property as null-able
        // https://stackoverflow.com/questions/67505347/non-nullable-property-must-contain-a-non-null-value-when-exiting-constructor-co

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }

    public class UserCreateCommandHandler : CommandHandler<UserCreateCommand, Guid>
    {
        #region DI & Ctor's
        public IUserRepository _userRepository { get; set; }

        public UserCreateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            // Create new user instance.
            var user = User.CreateNewInstance(request.Email, request.PhoneNumber);
            // Set password hash.
            user.Password = PasswordHash.Parse(request.Password);
            // Set full name.
            user.FullName = new FullName(request.Name, request.Surname);
            // Add to database.
            bool wasSaved = await _userRepository.AddAsync(user);
            if (wasSaved)
            {
                // TODO:
                // Log error.
            }
            return user.Id;
        }
    }

    public class UserCreateCommandValidator : BaseFluentValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("name can not be empty !");
            RuleFor(u => u.Surname).NotEmpty().WithMessage("Surname can not be empty !");
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .Matches(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$")
                .WithMessage("Email is required !");
            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .Length(11)
                .WithMessage("PhoneNumber must be 11 character.")
                .NotNull()
                .WithMessage("PhoneNumber is required !");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password can not be empty!");
        }
    }
}