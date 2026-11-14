using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Data;

namespace MonAmourBistro.Controllers;

public class MenuController : Controller
{
    private readonly AppDbContext _db;

    public MenuController(AppDbContext db)
    {
        _db = db;
    }

    // Meniul public: afișează doar preparatele disponibile, din baza de date.
    [HttpGet("/meniu")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Meniu";
        var preparate = await _db.Preparate
            .Where(p => p.Disponibil)
            .OrderBy(p => p.Categorie).ThenBy(p => p.Denumire)
            .ToListAsync();
        return View(preparate);
    }
}