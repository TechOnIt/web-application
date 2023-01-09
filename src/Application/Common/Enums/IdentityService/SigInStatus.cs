using TechOnIt.Domain.Common;

namespace TechOnIt.Application.Common.Enums.IdentityService;

public class SigInStatus : Enumeration
{
    #region Ctors
    public SigInStatus()
    {

    }

    public SigInStatus(int id, string name)
        : base(id, name)
    {
    }
    #endregion

    public static readonly SigInStatus NotFound = new(1, nameof(NotFound));
    public static readonly SigInStatus WrongInformations = new(2, nameof(WrongInformations));
    public static readonly SigInStatus LockUser = new(3, nameof(LockUser));
    public static readonly SigInStatus WrongPassowrd = new(4, nameof(WrongPassowrd));
    public static readonly SigInStatus Succeeded = new(5, nameof(Succeeded));
    public static readonly SigInStatus DuplicateUser = new(6, nameof(DuplicateUser));
    public static readonly SigInStatus Error = new(7, nameof(Error));
    public static readonly SigInStatus SmsError = new(8, nameof(SmsError));
}