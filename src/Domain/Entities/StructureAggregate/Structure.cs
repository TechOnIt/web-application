using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Entities.StructureAggregate;

/// <summary>
/// this class is aggregate root of Structure aggregate
/// </summary>
public class Structure
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
    public string? ConcurrencyStamp { get; private set; }

    #region Relations and Foreign key
    public Guid UserId { get; private set; }
    public virtual User? User { get; private set; }
    public virtual ICollection<Place>? Places { get; private set; }
    #endregion

    #region Constructure
    private Structure() { }
    public Structure(string name, PasswordHash password, Guid userId, StructureType type)
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
    /// Check concurrency token is valid?
    /// </summary>
    /// <param name="concurrencyStamp">Concurrency stamp string.</param>
    public bool IsConcurrencyStampValid(string concurrencyStamp)
        => ConcurrencyStamp == concurrencyStamp;
    #endregion

    #region Place Aggregate Methods
    /// <summary>
    /// Add place for this structure.
    /// </summary>
    /// <param name="place">Place object model.</param>
    public void AddPlace(Place place)
    {
        if (Places is null)
            Places = new List<Place>();
        Places.Add(place);
    }
    /// <summary>
    /// Remove an specific place.
    /// </summary>
    /// <param name="place">Place object model you want to remove it.</param>
    public void RemovePlace(Place place)
    {
        if (Places is null)
            throw new ArgumentNullException("Structure dosen't have any places.");
        Places.Remove(place);
    }
    /// <summary>
    /// Add range place to structure.
    /// </summary>
    /// <param name="places">List of place object model.</param>
    public void AddRangePlace(ICollection<Place>? places)
    {
        if (places is null) throw new ArgumentNullException("Places list is null here!");
        if (Places is null)
            Places = new List<Place>();
        foreach (var item in places)
            Places.Add(item);
    }
    #endregion
}