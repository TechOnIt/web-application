using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Common.Models.ViewModels.Places;

public class PlaceWithDevicesViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<DeviceCardViewModel>? Devices { get; set; }
}