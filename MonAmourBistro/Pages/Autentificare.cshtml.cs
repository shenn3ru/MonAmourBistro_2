using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonAmourBistro.Controllers;
using MonAmourBistro.Models.Formulare;

namespace MonAmourBistro.Pages;

public class AutentificareModel : PageModel
{
    [BindProperty]
    public AutentificareInput Input { get; set; } = new();

    public string? Mesaj { get; private set; }
    public string Continua { get; private set; } = "/";

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Validarea pe server: [Required], [EmailAddress], [StringLength].
        if (!ModelState.IsValid)
        {
            return Page(); // se afișează mesajele de eroare lângă câmpuri
        }

        // Autentificare simulată: nu se verifică parola într-o bază de date.
        var esteAdmin = Input.Email.Trim().StartsWith("admin", StringComparison.OrdinalIgnoreCase);
        Response.Cookies.Append(Sim.CookieName, esteAdmin ? "admin" : "client", new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });

        Mesaj = $"Autentificare reușită! Bun venit, {Input.Email.Trim()}. Rol: {(esteAdmin ? "Administrator" : "Client")}.";
        Continua = esteAdmin ? "/admin" : "/cont";
        ModelState.Clear();
        Input = new AutentificareInput();
        return Page();
    }
}
