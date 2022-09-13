using iot.Domain.Enums;

namespace iot.Application.Common.Models;

public record SensorViewModel(Guid Id, SensorType SensorType, Guid PlaceId);
