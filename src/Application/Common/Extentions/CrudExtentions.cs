using iot.Application.Common.Enums.IdentityServiceEnums;

namespace iot.Application.Common.Extentions;

public static class CrudExtentions
{
    public static bool IsSucceeded(this IdentityCrudStatus status)
        => status == IdentityCrudStatus.Succeeded ? true : false;

    public static bool IsFailed(this IdentityCrudStatus status)
    => status == IdentityCrudStatus.Failed ? true : false;

    public static bool IsNotFound(this IdentityCrudStatus status)
    => status == IdentityCrudStatus.NotFound ? true : false;

    public static bool IsDuplicate(this IdentityCrudStatus status)
    => status == IdentityCrudStatus.Duplicate ? true : false;

    public static bool ISCantRemove(this IdentityCrudStatus status)
    => status == IdentityCrudStatus.CantRemove ? true : false;
}
