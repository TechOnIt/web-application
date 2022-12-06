using iot.Domain.Enums;

namespace iot.Application.Common.ViewModels.Sensors;

public record SensorViewModel(Guid Id, SensorType SensorType, Guid PlaceId);