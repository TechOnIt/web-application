using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Services.Authenticateion;

public class UserService : IUserService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;

    public UserService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<(Guid? UserId, IdentityCrudStatus Status)> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user.PhoneNumber is null)
            return (null, IdentityCrudStatus.Failed);

        var isDuplicate = await _unitOfWorks.UserRepository.IsExistsByPhoneNumberAsync(user.PhoneNumber);
        if (isDuplicate)
            return (null, IdentityCrudStatus.Duplicate);

        await _unitOfWorks.UserRepository.CreateAsync(user, cancellationToken);
        if (user is null)
            return (null, IdentityCrudStatus.Failed);
        else
            return (user.Id, IdentityCrudStatus.Succeeded);
    }

    public async Task<IdentityCrudStatus> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var isExists = await IsExistsUserAsync(userId, cancellationToken);
        if (isExists)
            return IdentityCrudStatus.NotFound;

        var taskResult = _unitOfWorks.UserRepository.DeleteByIdAsync(userId, cancellationToken).IsCompletedSuccessfully;
        if (!taskResult)
            return IdentityCrudStatus.Failed;

        return IdentityCrudStatus.Succeeded;
    }

    public async Task<IdentityCrudStatus> DeleteUserAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var isExists = await _unitOfWorks.UserRepository.IsExistsByPhoneNumberAsync(phoneNumber);
        if (isExists)
            return IdentityCrudStatus.NotFound;

        var taskResult = _unitOfWorks.UserRepository.DeleteByPhoneNumberAsync(phoneNumber, cancellationToken).IsCompletedSuccessfully;
        if (!taskResult)
            return IdentityCrudStatus.Failed;

        return IdentityCrudStatus.Succeeded;
    }

    public async Task<UserViewModel> FindUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWorks.UserRepository.FindByIdAsync(userId);
        return user.Adapt<UserViewModel>();
    }

    public async Task<(UserViewModel User, IdentityCrudStatus Status)> UpdateUserAsync(UserViewModel user, CancellationToken cancellationToken = default)
    {
        var isDuplicate = await _unitOfWorks.UserRepository.IsExistsByPhoneNumberAsync(user.PhoneNumber);
        if (!isDuplicate)
            return (user, IdentityCrudStatus.NotFound);

        var result = _unitOfWorks.UserRepository.UpdateAsync(user.Adapt<User>(), cancellationToken).IsCompletedSuccessfully;
        if (!result)
            return (user, IdentityCrudStatus.Failed);

        return (user, IdentityCrudStatus.Succeeded);
    }

    public async Task<bool> IsExistsUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _unitOfWorks.UserRepository.IsExistsByIdAsync(userId, cancellationToken);
}