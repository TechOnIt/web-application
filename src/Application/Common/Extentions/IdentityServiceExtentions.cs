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
}
