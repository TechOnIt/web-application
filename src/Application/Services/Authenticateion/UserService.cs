using iot.Application.Common.Enums.IdentityServiceEnums;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;
using iot.Domain.Entities.Identity.UserAggregate;
using iot.Infrastructure.Common.Encryptions.Contracts;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;

namespace iot.Application.Services.Authenticateion;

public class UserService : IUserService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;

    public UserService(IUnitOfWorks unitOfWorks, IEncryptionHandlerService encryptionHandler)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<(Guid? UserId, IdentityCrudStatus Status)> CreateUserAsync(UserViewModel user, CancellationToken cancellationToken = default)
    {
        if (user.PhoneNumber is null)
            return (null, IdentityCrudStatus.Failed);

        var isDuplicate = await _unitOfWorks.UserRepository.IsExistsUserByPhoneNumberAsync(user.PhoneNumber);
        if (isDuplicate)
            return (null, IdentityCrudStatus.Duplicate);

        var model = user.Adapt<User>();
        var result = await _unitOfWorks.UserRepository.CreateNewUser(model, cancellationToken);
        if (result is null)
            return (null, IdentityCrudStatus.Failed);
        else
            return (result.Id, IdentityCrudStatus.Succeeded);
    }

    public async Task<IdentityCrudStatus> DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var isExists = await IsExistsUserAsync(userId, cancellationToken);
        if (isExists)
            return (IdentityCrudStatus.NotFound);

        var taskResult = _unitOfWorks.UserRepository.DeleteUserByIdAsync(userId, cancellationToken).IsCompletedSuccessfully;
        if (!taskResult)
            return IdentityCrudStatus.Failed;

        return IdentityCrudStatus.Succeeded;
    }

    public async Task<IdentityCrudStatus> DeleteUserAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        var isExists = await _unitOfWorks.UserRepository.IsExistsUserByPhoneNumberAsync(phoneNumber);
        if (isExists)
            return (IdentityCrudStatus.NotFound);

        var taskResult = _unitOfWorks.UserRepository.DeleteUserByPhoneNumberAsync(phoneNumber, cancellationToken).IsCompletedSuccessfully;
        if (!taskResult)
            return IdentityCrudStatus.Failed;

        return IdentityCrudStatus.Succeeded;
    }

    public async Task<UserViewModel> FindUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWorks.UserRepository.FindUserByUserIdAsync(userId);
        return user.Adapt<UserViewModel>();
    }

    public async Task<(UserViewModel User, IdentityCrudStatus Status)> UpdateUserAsync(UserViewModel user, CancellationToken cancellationToken = default)
    {
        var isDuplicate = await _unitOfWorks.UserRepository.IsExistsUserByPhoneNumberAsync(user.PhoneNumber);
        if (!isDuplicate)
            return (user, IdentityCrudStatus.NotFound);

        var result = _unitOfWorks.UserRepository.UpdateUserAsync(user.Adapt<User>(), cancellationToken).IsCompletedSuccessfully;
        if (!result)
            return (user, IdentityCrudStatus.Failed);

        return (user, IdentityCrudStatus.Succeeded);
    }

    public async Task<bool> IsExistsUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _unitOfWorks.UserRepository.IsExistsUserByIdAsync(userId, cancellationToken);
}
