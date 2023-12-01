using TechOnIt.Application.Commands.Structures.Dashboard.GetMyStructures;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Components;

public class StructureCardListViewComponent : ViewComponent
{
    private readonly IMediator _mediator;
    public StructureCardListViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = User as ClaimsPrincipal;
        if (user is null) return View();

        var currentUser = await user.GetCurrentUserIdAsync();
        var myStructures = await _mediator.Send(new GetMyStructuresCardCommand { UserId = currentUser.Id });
        return View(myStructures);
    }
}
