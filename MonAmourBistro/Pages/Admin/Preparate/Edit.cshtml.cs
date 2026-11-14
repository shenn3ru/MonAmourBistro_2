using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Controllers;
using MonAmourBistro.Data;
using MonAmourBistro.Models;

namespace MonAmourBistro.Pages.Admin.Preparate;

[SimAuthorize(SimRole.Administrator)]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;

    public EditModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Preparat Preparat { get; set; } = new();

    public SelectList Categorii => new(Preparat.Categorii);

    [TempData]
    public string? Mesaj { get; set; }

    [TempData]
    public string? Eroare { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var existent = await _db.Preparate.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (existent == null)
        {
            Eroare = $"Preparatul cu identificatorul {id} nu există.";
            return RedirectToPage("Index");
        }

        Preparat = existent; // formularul se completează cu valorile existente
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        // Identificatorul se ia din adresă, nu din formular: nu poate fi modificat.
        Preparat.Id = id;

        var existent = await _db.Preparate.FirstOrDefaultAsync(p => p.Id == id);
        if (existent == null)
        {
            Eroare = $"Preparatul cu identificatorul {id} nu mai există.";
            return RedirectToPage("Index");
        }

        if (!string.IsNullOrWhiteSpace(Preparat.Categorie) && !Models.Preparat.Categorii.Contains(Preparat.Categorie))
        {
            ModelState.AddModelError("Preparat.Categorie", "Categoria aleasă nu este permisă.");
        }
        if (!string.IsNullOrWhiteSpace(Preparat.Denumire))
        {
            var denumire = Preparat.Denumire.Trim().ToLower();
            if (await _db.Preparate.AnyAsync(p => p.Id != id && p.Denumire.ToLower() == denumire))
            {
                ModelState.AddModelError("Preparat.Denumire", "Există deja un preparat cu această denumire.");
            }
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Copiem valorile noi în înregistrarea din baza de date (Id-ul rămâne același).
        existent.Denumire = Preparat.Denumire.Trim();
        existent.Categorie = Preparat.Categorie;
        existent.Descriere = Preparat.Descriere;
        existent.Pret = Preparat.Pret;
        existent.Gramaj = Preparat.Gramaj;
        existent.Disponibil = Preparat.Disponibil;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Modificările nu au putut fi salvate în baza de date. Încercați din nou.");
            return Page();
        }

        Mesaj = $"Modificările pentru „{existent.Denumire}” au fost salvate.";
        return RedirectToPage("Index");
    }
}
