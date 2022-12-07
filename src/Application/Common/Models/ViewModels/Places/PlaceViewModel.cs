namespace iot.Application.Common.Models.ViewModels.Places;

public record PlaceViewModel(Guid Id, string Name, string Description, DateTime Createdate, DateTime Modifydate, Guid StructureId);