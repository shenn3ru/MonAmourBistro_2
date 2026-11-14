using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Controllers;
using MonAmourBistro.Data;
using MonAmourBistro.Models;

namespace MonAmourBistro.Pages.Admin.Preparate;

[SimAuthorize(SimRole.Administrator)]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db)
    {
        _db = db;
    }

    public Preparat Preparat { get; private set; } = new();

    [TempData]
    public string? Mesaj { get; set; }

    [TempData]
    public string? Eroare { get; set; }

    // GET: afișează confirmarea. Nu se șterge nimic.
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var existent = await _db.Preparate.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (existent == null)
        {
            Eroare = $"Preparatul cu identificatorul {id} nu există.";
            return RedirectToPage("Index");
        }

        Preparat = existent;
        return Page();
    }

    // POST: ștergerea din baza de date se face doar după confirmarea utilizatorului.
    public async Task<IActionResult> OnPostAsync(int id)
    {
        var existent = await _db.Preparate.FirstOrDefaultAsync(p => p.Id == id);
        if (existent == null)
        {
            Eroare = $"Preparatul cu identificatorul {id} nu mai există.";
            return RedirectToPage("Index");
        }

        _db.Preparate.Remove(existent);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Eroare = $"Preparatul „{existent.Denumire}” nu a putut fi șters. Încercați din nou.";
            return RedirectToPage("Index");
        }

        Mesaj = $"Preparatul „{existent.Denumire}” a fost șters.";
        return RedirectToPage("Index");
    }
}
