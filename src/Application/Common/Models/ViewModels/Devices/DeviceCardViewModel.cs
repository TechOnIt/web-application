using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Devices;

public class DeviceCardViewModel
{
    public string Id { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsHigh { get; set; }
}