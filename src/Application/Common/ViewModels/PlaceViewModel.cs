namespace iot.Application.Common.ViewModels;

public record PlaceViewModel(Guid Id, string Name, string Description, DateTime Createdate, DateTime Modifydate, Guid StructureId);