using iot.Domain.Entities.Identity;

namespace iot.Application.Repositories.Users;

public class UserRepository : SqlRepository<User>, IUserRepository
{
}