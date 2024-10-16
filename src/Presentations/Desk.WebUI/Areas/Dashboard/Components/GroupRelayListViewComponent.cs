using TechOnIt.Application.Common.Models.ViewModels.Relay;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class GroupRelayListViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IList<RelayCardControlViewModel> relays)
        => await Task.FromResult(View(relays));
}