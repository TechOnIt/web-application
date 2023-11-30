using Microsoft.AspNetCore.Mvc;

namespace Desk.Controllers
{
    public class PlaceController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}