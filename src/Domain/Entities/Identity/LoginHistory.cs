using iot.Domain.ValueObjects;
using System;

namespace iot.Domain.Entities.Identity;

public class LoginHistory
{
    #region Constructures
    public LoginHistory(DateTime createdDateTime, IPv4 ip, Guid? id)
    {
        Id = id ?? Guid.NewGuid();
        CreatedDateTime = createdDateTime;
        Ip = ip;
    }
    #endregion

    public Guid? Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public IPv4 Ip { get; set; }

    #region Relations
    public Guid UserId { get; set; }  // Foreign key

    public virtual User User { get; set; }
    #endregion
}