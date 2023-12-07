using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Desk.Web.Areas.Dashboard
{
    [Area("Dashboard")]
    public class ScheduleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
