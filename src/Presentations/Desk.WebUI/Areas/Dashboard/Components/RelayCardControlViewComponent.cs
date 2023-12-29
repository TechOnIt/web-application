using TechOnIt.Application.Common.Models.ViewModels.Relay;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class RelayCardControlViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(RelayCardControlViewModel device)
        => await Task.FromResult(View(device));
}