using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Application.Common.Models.ViewModels.Structures;

public class StructurePlacesWithRelayViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<PlaceWithRelayViewModel>? Places { get; set; }
}