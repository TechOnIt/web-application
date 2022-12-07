using TechOnIt.Domain.Common;
using System;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Domain.Entities.Identity.UserAggregate;

public class LoginHistory
{
    #region Constructures
    LoginHistory() { }

    public LoginHistory(IPv4 ip, Guid userId)
    {
        Id = Guid.NewGuid();
        Ip = ip;
        UserId = userId;
        CreatedDateTime = DateTime.Now;
    }
    #endregion

    public Guid? Id { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public IPv4 Ip { get; private set; }

    #region Relations
    public Guid UserId { get; private set; }  // Foreign key
    public virtual User User { get; private set; }
    #endregion
}