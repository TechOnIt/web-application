namespace TechOnIt.Application.Common.Models.ViewModels.Places;

public record PlaceViewModel(Guid Id, string Name, string Description, DateTime Createdate, DateTime Modifydate, Guid StructureId);
public record PlaceUpdateViewModel(Guid Id, string Name, string Description, Guid StructureId);
public record PlaceCreateViewModel(Guid Id, string Name, string Description, Guid StructureId);