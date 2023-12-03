using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class PlaceDeviceListViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IList<DeviceCardControlViewModel> devices)
        => await Task.FromResult(View(devices));
}