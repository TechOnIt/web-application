using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Desk.WebUI.Controllers;

public class AuthenticationController : Controller
{
    [HttpGet]
    public IActionResult Signin()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Signout()
    {
        return Redirect("/");
    }
}