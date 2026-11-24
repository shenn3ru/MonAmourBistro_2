using Microsoft.AspNetCore.Mvc;

namespace MonAmourBistro.Controllers;

public class AccountController : Controller
{
    [HttpGet("/inregistrare")]
    public IActionResult Register()
    {
        // În simulare, înregistrarea și autentificarea sunt aceeași pagină.
        return Redirect("/autentificare");
    }

    // Pagina /autentificare este acum o pagină Razor: Pages/Autentificare.cshtml

    [HttpGet("/cont")]
    [SimAuthorize(SimRole.Client)]
    public IActionResult MyAccount()
    {
        ViewData["Title"] = "Contul meu";
        return View();
    }
}
