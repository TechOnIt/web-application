using iot.Domain.Enums;

namespace iot.Application.Common.ViewModels;

public record SensorViewModel(Guid Id, SensorType SensorType, Guid PlaceId);