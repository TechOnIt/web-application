using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Entities.Identity;

public class DynamicAccess
{
    private DynamicAccess()
    {
            
    }

    public DynamicAccess(Guid Id,string Path, DateTime CreateDate, Guid UserId, Guid RoleId, Guid CreateBy)
    {
        ValidatePath(Path);
        this.Id = Id;
        this.Path = Path;
        this.CreateDate = CreateDate;
        this.UserId = UserId;
        this.CreateBy = CreateBy;
    }

    public DynamicAccess(string Path, Guid UserId, Guid RoleId, Guid CreateBy)
    {
        ValidatePath(Path);
        this.Id = Guid.NewGuid();
        this.Path = Path;
        this.CreateDate = DateTime.Now;
        this.UserId = UserId;
        this.CreateBy = CreateBy;
    }

    #region methods
    private void ValidatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
    }
    #endregion

    public Guid Id { get; private set; }
    public string Path { get; private set; }
    public DateTime CreateDate { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CreateBy { get; private set; }

    #region relations
    public virtual User User { get; set; }
    #endregion
}
