using iot.Application.Common.Enums.IdentityService;
using iot.Application.Common.ViewModels.Users;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IUserService
{
    Task<(Guid? UserId, IdentityCrudStatus Status)> CreateUserAsync(UserViewModel user,CancellationToken cancellationToken = default);
    Task<(UserViewModel User, IdentityCrudStatus Status)> UpdateUserAsync(UserViewModel user,CancellationToken cancellationToken = default);
    Task<IdentityCrudStatus> DeleteUserAsync(Guid userId,CancellationToken cancellationToken = default);
    Task<IdentityCrudStatus> DeleteUserAsync(string phoneNumber,CancellationToken cancellationToken = default);
    Task<bool> IsExistsUserAsync(Guid userId,CancellationToken cancellationToken = default);
    Task<UserViewModel> FindUserByIdAsync(Guid userId,CancellationToken cancellationToken = default); 
}