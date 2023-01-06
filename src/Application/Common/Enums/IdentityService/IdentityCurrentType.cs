using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechOnIt.Domain.Common;

namespace TechOnIt.Application.Common.Enums.IdentityService;

public class IdentityCurrentType : Enumeration
{
    #region Ctors
    public IdentityCurrentType()
    {

    }

    public IdentityCurrentType(int id, string name)
        : base(id, name)
    {
    }
    #endregion

    public static readonly IdentityCurrentType NotAuthenticated = new(1, nameof(NotAuthenticated));
    public static readonly IdentityCurrentType User = new(2, nameof(User));
    public static readonly IdentityCurrentType Structure = new(1, nameof(Structure));

}
