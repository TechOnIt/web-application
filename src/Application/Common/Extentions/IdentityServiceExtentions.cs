using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Infrastructure.Common.Notifications.Results;
using TechOnIt.Application.Common.Enums.IdentityService;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Application.Common.Extentions;

public static class IdentityServiceExtentions
{
    #region common messages
    const string NotFound = "Not found user !";
    const string WrongInformations = "Username or password is wrong!";
    const string LockUser = "user is locked !";
    const string WrongPassowrd = "password is wrong!";
    const string SucceededValidations = "Welcome !";
    #endregion

    public static SigInStatus GetUserSignInStatusResult(this User user, string password = "")
    {
        if (user == null)
            return SigInStatus.NotFound;
        else if (user.IsBaned is true)
            return SigInStatus.WrongInformations;
        else if (user.LockOutDateTime != null)
            return SigInStatus.LockUser;
        else if (!string.IsNullOrWhiteSpace(password))
        {
            if (user.Password != PasswordHash.Parse(password))
                return SigInStatus.WrongPassowrd;
        }

        return SigInStatus.Succeeded;
    }

    public static (SigInStatus Status, string message) GetUserSignInStatusResultWithMessage(this User user, string password = "")
    {
        if (user == null)
            return (SigInStatus.NotFound, NotFound);
        else if (user.IsBaned is true)
            return (SigInStatus.WrongInformations, WrongInformations);
        else if (user.LockOutDateTime != null)
            return (SigInStatus.LockUser, LockUser);
        else if (!string.IsNullOrWhiteSpace(password))
        {
            if (!user.Password.VerifyPasswordHash(password))
                return (SigInStatus.WrongPassowrd, WrongPassowrd);
        }

        return (SigInStatus.Succeeded, SucceededValidations);
    }
    
    public static (SigInStatus Status, string message) GetStructureSignInStatusResultWithMessage(this Structure structure, string password = "")
    {
        if (structure == null)
            return (SigInStatus.NotFound, NotFound);
        else if (structure.IsActive is false)
            return (SigInStatus.WrongInformations, WrongInformations);
        else if (!string.IsNullOrWhiteSpace(password))
        {
            if (!structure.Password.VerifyPasswordHash(password))
                return (SigInStatus.WrongPassowrd, WrongPassowrd);
        }

        return (SigInStatus.Succeeded, SucceededValidations);
    }

    public static bool IsSendSuccessfully(this SendStatus status)
        => status == SendStatus.Successeded ? true : false;

    public static bool IsSucceeded(this SigInStatus status)
        => status == SigInStatus.Succeeded ? true : false;

    public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        return identity?.FindFirst(claimType)?.Value;
    }

    public static string FindFirstValue(this IIdentity identity, string claimType)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        return claimsIdentity?.FindFirstValue(claimType);
    }

    public static string GetUserId(this IIdentity identity)
    {
        return identity?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
    {
        var userId = identity?.GetUserId();
        return userId.HasValue() ? (T)Convert.ChangeType(userId, typeof(T), CultureInfo.InvariantCulture) : default(T);
    }

    public static string GetUserName(this IIdentity identity)
    {
        return identity?.FindFirstValue(ClaimTypes.Name);
    }

    public static bool HasValue(this string parameter)
        => parameter != null;
}
