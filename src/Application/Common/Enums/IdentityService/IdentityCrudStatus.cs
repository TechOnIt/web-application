using iot.Domain.Common;

namespace iot.Application.Common.Enums.IdentityServiceEnums;

public class IdentityCrudStatus : Enumeration
{
    #region Ctors
    public IdentityCrudStatus()
    {

    }

    public IdentityCrudStatus(int id, string name)
        : base(id, name)
    {
    }
    #endregion

    public static readonly IdentityCrudStatus NotFound = new(1, nameof(NotFound));
    public static readonly IdentityCrudStatus Duplicate = new(2, nameof(Duplicate));
    public static readonly IdentityCrudStatus CantRemove = new(3, nameof(CantRemove));
    public static readonly IdentityCrudStatus Succeeded = new(4, nameof(Succeeded));
    public static readonly IdentityCrudStatus Failed = new(5,nameof(Failed));
    public static readonly IdentityCrudStatus ServerError = new(6, nameof(ServerError));
}