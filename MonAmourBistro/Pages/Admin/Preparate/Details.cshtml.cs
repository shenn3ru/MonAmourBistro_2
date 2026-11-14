using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Controllers;
using MonAmourBistro.Data;
using MonAmourBistro.Models;

namespace MonAmourBistro.Pages.Admin.Preparate;

[SimAuthorize(SimRole.Administrator)]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db)
    {
        _db = db;
    }

    public Preparat Preparat { get; private set; } = new();

    [TempData]
    public string? Eroare { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var existent = await _db.Preparate.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (existent == null)
        {
            // Identificator inexistent: revenim la listă cu un mesaj, fără blocarea aplicației.
            Eroare = $"Preparatul cu identificatorul {id} nu există.";
            return RedirectToPage("Index");
        }

        Preparat = existent;
        return Page();
    }
}
