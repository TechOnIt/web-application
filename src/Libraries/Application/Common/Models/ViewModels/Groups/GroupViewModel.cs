namespace TechOnIt.Application.Common.Models.ViewModels.Groups;

public record GroupViewModel(Guid Id, string Name, string Description, DateTime Createdate, DateTime Modifydate, Guid StructureId);
public record GroupUpdateViewModel(Guid Id, string Name, string Description, Guid StructureId);
public record GroupCreateViewModel(Guid Id, string Name, string Description, Guid StructureId);