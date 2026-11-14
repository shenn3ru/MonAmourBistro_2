using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Controllers;
using MonAmourBistro.Data;
using MonAmourBistro.Models;

namespace MonAmourBistro.Pages.Admin.Preparate;

[SimAuthorize(SimRole.Administrator)]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Preparat> Preparate { get; private set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Cautare { get; set; }

    [TempData]
    public string? Mesaj { get; set; }

    [TempData]
    public string? Eroare { get; set; }

    public async Task OnGetAsync()
    {
        IQueryable<Preparat> query = _db.Preparate.AsNoTracking();

        // Căutarea după denumire se face în baza de date (WHERE ... LIKE).
        if (!string.IsNullOrWhiteSpace(Cautare))
        {
            var text = Cautare.Trim();
            query = query.Where(p => EF.Functions.Like(p.Denumire, "%" + text + "%"));
        }

        Preparate = await query.OrderBy(p => p.Denumire).ToListAsync();
    }
}
