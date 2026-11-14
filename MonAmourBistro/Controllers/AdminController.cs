using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Data;

namespace MonAmourBistro.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("")]
    [SimAuthorize(SimRole.Administrator)]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Panou de administrare";
        ViewData["NrPreparate"] = await _db.Preparate.CountAsync();
        ViewData["Section"] = "dashboard";
        return View("Index");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Redirect("/autentificare");
    }

    // Gestiunea meniului este acum modulul CRUD din Pages/Admin/Preparate.
    [HttpGet("meniu")]
    public IActionResult Menu()
    {
        return Redirect("/admin/preparate");
    }

    [HttpGet("pagini")]
    [SimAuthorize(SimRole.Administrator)]
    public IActionResult Pages()
    {
        ViewData["Title"] = "Gestiunea paginilor";
        ViewData["Section"] = "pages";
        return View("Index");
    }

    [HttpGet("rezervari")]
    [SimAuthorize(SimRole.Administrator)]
    public IActionResult Reservations()
    {
        ViewData["Title"] = "Lista rezervărilor";
        ViewData["Section"] = "reservations";
        return View("Index");
    }
}
