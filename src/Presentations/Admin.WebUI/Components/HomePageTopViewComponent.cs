namespace TechOnIt.Admin.WebUI.Components
{
    public class HomePageTopViewComponent : ViewComponent
    {
        public HomePageTopViewComponent()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
