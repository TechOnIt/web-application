using TechOnIt.Application.Common.Models.ViewModels.Groups;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class GroupsWithRelaysControlViewComponent : ViewComponent
{
    public GroupsWithRelaysControlViewComponent()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(List<GroupWithRelayViewModel> groups)
        => await Task.FromResult(View(groups));
}