using TechOnIt.Application.Common.Models.ViewModels.Structures;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class StructureCardViewComponent : ViewComponent
{
    public StructureCardViewComponent()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync(StructureCardViewModel structure) => await Task.FromResult(View(structure));
}