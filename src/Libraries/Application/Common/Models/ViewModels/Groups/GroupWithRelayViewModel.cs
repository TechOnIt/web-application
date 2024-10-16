using TechOnIt.Application.Common.Models.ViewModels.Relay;

namespace TechOnIt.Application.Common.Models.ViewModels.Groups;

public class GroupWithRelayViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<RelayCardControlViewModel>? Relays { get; set; }
}