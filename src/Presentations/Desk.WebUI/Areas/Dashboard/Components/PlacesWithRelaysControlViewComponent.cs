using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class PlacesWithRelaysControlViewComponent : ViewComponent
{
    public PlacesWithRelaysControlViewComponent()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(List<PlaceWithRelayViewModel> places)
        => await Task.FromResult(View(places));
}