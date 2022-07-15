using iot.Domain.ValueObjects;
using System;

namespace iot.Domain.Entities.Identity;

public class LoginHistory
{
    #region Constructures
    public LoginHistory(DateTime createdDateTime, string ip, Guid? id)
    {
        CreatedDateTime = createdDateTime;
        Ip = ip;
        Id = id ?? Guid.NewGuid();
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