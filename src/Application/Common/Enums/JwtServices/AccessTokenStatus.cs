using TechOnIt.Domain.Common;

namespace TechOnIt.Application.Common.Enums.JwtServices;

public class AccessTokenStatus : Enumeration
{
    #region Ctors
    public AccessTokenStatus() { }

    public AccessTokenStatus(int id, string name)
        : base(id, name) { }
    #endregion

    public static readonly AccessTokenStatus Succeeded = new(1, nameof(Succeeded));
    public static readonly AccessTokenStatus WrongInformations = new(2, nameof(WrongInformations));
    public static readonly AccessTokenStatus Expired = new(4, nameof(Expired));
    public static readonly AccessTokenStatus Conflict = new(4, nameof(Conflict));
}