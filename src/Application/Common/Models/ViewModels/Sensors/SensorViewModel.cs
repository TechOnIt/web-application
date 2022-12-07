using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Sensors;

public record SensorViewModel(Guid Id, SensorType SensorType, Guid PlaceId);