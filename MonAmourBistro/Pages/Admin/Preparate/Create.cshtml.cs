using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Controllers;
using MonAmourBistro.Data;
using MonAmourBistro.Models;

namespace MonAmourBistro.Pages.Admin.Preparate;

[SimAuthorize(SimRole.Administrator)]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;

    public CreateModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Preparat Preparat { get; set; } = new();

    public SelectList Categorii => new(Preparat.Categorii);

    [TempData]
    public string? Mesaj { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Validări suplimentare, pe lângă atributele [Required], [StringLength], [Range] din model.
        if (!string.IsNullOrWhiteSpace(Preparat.Categorie) && !Models.Preparat.Categorii.Contains(Preparat.Categorie))
        {
            ModelState.AddModelError("Preparat.Categorie", "Categoria aleasă nu este permisă.");
        }
        if (!string.IsNullOrWhiteSpace(Preparat.Denumire))
        {
            var denumire = Preparat.Denumire.Trim().ToLower();
            if (await _db.Preparate.AnyAsync(p => p.Denumire.ToLower() == denumire))
            {
                ModelState.AddModelError("Preparat.Denumire", "Există deja un preparat cu această denumire.");
            }
        }

        if (!ModelState.IsValid)
        {
            // Formularul este afișat din nou, cu valorile introduse și mesajele de eroare.
            return Page();
        }

        Preparat.Id = 0; // Id-ul este generat automat de baza de date
        Preparat.Denumire = Preparat.Denumire.Trim();
        _db.Preparate.Add(Preparat);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Preparatul nu a putut fi salvat în baza de date. Verificați datele și încercați din nou.");
            return Page();
        }

        Mesaj = $"Preparatul „{Preparat.Denumire}” a fost adăugat cu succes.";
        return RedirectToPage("Index");
    }
}
