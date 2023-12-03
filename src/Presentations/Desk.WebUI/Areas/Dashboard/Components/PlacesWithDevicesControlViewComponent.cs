using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class PlacesWithDevicesControlViewComponent : ViewComponent
{
    public PlacesWithDevicesControlViewComponent()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(List<PlaceWithDevicesViewModel> places)
        => await Task.FromResult(View(places));
}