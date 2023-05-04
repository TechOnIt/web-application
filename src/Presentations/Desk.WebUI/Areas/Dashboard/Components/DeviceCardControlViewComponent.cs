using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Components;

public class DeviceCardControlViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(DeviceCardControlViewModel device)
        => await Task.FromResult(View(device));
}