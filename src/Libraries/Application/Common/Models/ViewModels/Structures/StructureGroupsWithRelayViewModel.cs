using TechOnIt.Application.Common.Models.ViewModels.Groups;

namespace TechOnIt.Application.Common.Models.ViewModels.Structures;

public class StructureGroupsWithRelayViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<GroupWithRelayViewModel>? Groups { get; set; }
}