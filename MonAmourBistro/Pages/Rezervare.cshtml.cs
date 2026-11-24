using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonAmourBistro.Controllers;
using MonAmourBistro.Models.Formulare;

namespace MonAmourBistro.Pages;

// Formularul specific temei (restaurant): rezervarea unei mese. Doar pentru clienți autentificați.
[SimAuthorize(SimRole.Client)]
public class RezervareModel : PageModel
{
    private static readonly TimeSpan Deschidere = new(10, 0, 0);
    private static readonly TimeSpan Inchidere = new(22, 0, 0);

    [BindProperty]
    public RezervareInput Input { get; set; } = new();

    [TempData]
    public string? Confirmare { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Reguli suplimentare, specifice restaurantului.
        if (Input.Data.HasValue && Input.Data.Value.Date < DateTime.Today)
        {
            ModelState.AddModelError("Input.Data", "Data rezervării nu poate fi în trecut.");
        }
        if (Input.Ora.HasValue && (Input.Ora < Deschidere || Input.Ora > Inchidere))
        {
            ModelState.AddModelError("Input.Ora", "Rezervările se fac între 10:00 și 22:00.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Confirmare = $"Rezervarea pe numele {Input.Nume.Trim()} a fost înregistrată: " +
                     $"{Input.Data:dd.MM.yyyy}, ora {Input.Ora:hh\\:mm}, {Input.NrPersoane} persoane. Vă vom contacta la {Input.Telefon.Trim()}.";
        return RedirectToPage();
    }
}
