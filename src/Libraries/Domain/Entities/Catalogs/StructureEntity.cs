using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Entities.Catalogs;

/// <summary>
/// this class is aggregate root of Structure aggregate
/// </summary>
public class StructureEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public StructureType Type { get; private set; } = StructureType.Home;
    public Concurrency ApiKey { get; private set; } = Concurrency.NewToken();
    public PasswordHash? Password { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? ModifiedAt { get; private set; }
    public bool IsActive { get; private set; } = true;
    public byte[] ConcurrencyStamp { get; private set; } = new byte[0];

    #region Relations and Foreign key
    public Guid UserId { get; private set; }
    public virtual UserEntity? User { get; private set; }
    public virtual ICollection<GroupEntity>? Groups { get; private set; }
    #endregion

    #region Constructure
    private StructureEntity() { }
    public StructureEntity(string name, PasswordHash password, Guid userId, StructureType type)
    {
        SetName(name);
        SetPassword(password);
        UserId = userId;
        Type = type;
    }
    #endregion

    #region Methods
    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Structure name cannot be null.");
        Name = name;
    }
    /// <summary>
    /// Set new password for structure.
    /// </summary>
    /// <param name="newPassword"></param>
    private void SetPassword(PasswordHash newPassword)
    {
        Password = newPassword;
    }
    /// <summary>
    /// Generate new api key for structure.
    /// </summary>
    public void GenerateApiKey()
    {
        ApiKey = Concurrency.NewToken();
    }
    /// <summary>
    /// Active this structure.
    /// </summary>
    public void Active()
    {
        if (IsActive)
            throw new ArgumentOutOfRangeException("This structure already is active.");
        IsActive = true;
    }
    /// <summary>
    /// Deactive this structure.
    /// </summary>
    public void Deactive()
    {
        if (!IsActive)
            throw new ArgumentOutOfRangeException("This structure is already deactive.");
        IsActive = false;
    }
    /// <summary>
    /// Check row version is validate?
    /// </summary>
    public bool IsConcurrencyStampValidate(string concurrencyStamp)
        => ConcurrencyStamp == Encoding.ASCII.GetBytes(concurrencyStamp);
    #endregion

    #region Group Aggregate Methods
    /// <summary>
    /// Add group for this structure.
    /// </summary>
    /// <param name="group">Group object model.</param>
    public void AddGroup(GroupEntity group)
    {
        if (Groups is null)
            Groups = new List<GroupEntity>();
        Groups.Add(group);
    }
    /// <summary>
    /// Remove an specific group.
    /// </summary>
    /// <param name="group">Group object model you want to remove it.</param>
    public void RemoveGroup(GroupEntity group)
    {
        if (Groups is null)
            throw new ArgumentNullException("Structure dosen't have any groups.");
        Groups.Remove(group);
    }
    /// <summary>
    /// Add range group to structure.
    /// </summary>
    /// <param name="groups">List of group object model.</param>
    public void AddRangeGroup(ICollection<GroupEntity>? groups)
    {
        if (groups is null) throw new ArgumentNullException("Groups list is null here!");
        if (Groups is null)
            Groups = new List<GroupEntity>();
        foreach (var item in groups)
            Groups.Add(item);
    }
    #endregion
}