using Microsoft.AspNetCore.Mvc;

namespace Desk.Controllers
{
    public class StructureController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}