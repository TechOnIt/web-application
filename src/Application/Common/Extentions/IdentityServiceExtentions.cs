using iot.Application.Common.Enums.IdentityService;
using iot.Infrastructure.Common.Notifications.Results;

namespace iot.Application.Common.Extentions;

public static class IdentityServiceExtentions
{
    #region common messages
    const string NotFound = "Not found user !";
    const string WrongInformations = "Username or password is wrong!";
    const string LockUser = "user is locked !";
    const string WrongPassowrd = "password is wrong!";
    const string SucceededUserValidations = "Welcome !";
    #endregion

    public static SigInStatus GetUserSignInStatusResult(this User user,string password="")
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

    public static (SigInStatus Status,string message) GetUserSignInStatusResultWithMessage(this User user, string password = "")
    {
        if (user == null)
            return (SigInStatus.NotFound,NotFound);
        else if (user.IsBaned is true)
            return (SigInStatus.WrongInformations,WrongInformations);
        else if (user.LockOutDateTime != null)
            return (SigInStatus.LockUser,LockUser);
        else if (!string.IsNullOrWhiteSpace(password))
        {
            if (user.Password != PasswordHash.Parse(password))
                return (SigInStatus.WrongPassowrd,WrongPassowrd);
        }

        return (SigInStatus.Succeeded,SucceededUserValidations);
    }

    public static bool SendSuccessfully(this SendStatus status)
        => status == SendStatus.Successeded ? true : false;

    public static bool IsSucceeded(this SigInStatus status)
        => status == SigInStatus.Succeeded ? true : false;
}
