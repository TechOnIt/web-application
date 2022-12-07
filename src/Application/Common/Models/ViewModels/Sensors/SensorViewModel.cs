using iot.Domain.Enums;

namespace iot.Application.Common.Models.ViewModels.Sensors;

public record SensorViewModel(Guid Id, SensorType SensorType, Guid PlaceId);