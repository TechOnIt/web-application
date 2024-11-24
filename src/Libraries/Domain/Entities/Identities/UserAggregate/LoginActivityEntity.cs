namespace TechOnIt.Domain.Entities.Identities.UserAggregate;

public class LoginActivityEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public IPv4? Ip { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    #region Relations
    public Guid UserId { get; private set; }  // Foreign key
    public virtual UserEntity? User { get; private set; }
    #endregion

    #region Ctor
    private LoginActivityEntity() { }
    public LoginActivityEntity(IPv4 ip, Guid userId)
    {
        Ip = ip;
        UserId = userId;
    }
    #endregion
}