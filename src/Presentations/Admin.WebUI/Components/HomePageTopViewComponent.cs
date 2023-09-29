namespace TechOnIt.Admin.WebUI.Components
{
    public class HomePageTopViewComponent : ViewComponent
    {
        public HomePageTopViewComponent()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(15000);
            return View();
        }
    }
}
