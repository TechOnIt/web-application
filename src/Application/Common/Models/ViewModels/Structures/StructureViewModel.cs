using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Structures;

public class StructureViewModel
{
    public StructureViewModel(Guid id, string name, string description, bool isActive, StuctureType type, DateTime? modifyDate)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        Type = type;
        CreateDate = DateTime.Now;
        ModifyDate = modifyDate;
    }

    public StructureViewModel()
    {

    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public StuctureType Type { get; set; }
    public Concurrency ApiKey { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }

}