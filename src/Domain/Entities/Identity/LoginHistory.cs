using iot.Domain.Common;
using iot.Domain.ValueObjects;
using System;

namespace iot.Domain.Entities.Identity;

public class LoginHistory : IEntity
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