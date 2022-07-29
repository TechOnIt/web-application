using iot.Application.Repositories.SQL.Users;
using iot.Domain.Entities.Identity;
using MediatR;

namespace iot.Application.Commands.Users
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Guid>
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
            var user = new User(request.Email, request.PhoneNumber, request.Password, request.Surname, request.Name);
            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}