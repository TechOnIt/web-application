using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Common.Models.ViewModels.Relay;

public class RelayCardControlViewModel
{
    public string Id { get; set; }
    public int Pin { get; set; }
    public RelayType RelayType { get; set; }
    public bool IsHigh { get; set; }
}