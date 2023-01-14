using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Application.Common.Models.ViewModels.Structures;

public class StructurePlacesWithDevicesViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PlaceWithDevicesViewModel> Places { get; set; }
}