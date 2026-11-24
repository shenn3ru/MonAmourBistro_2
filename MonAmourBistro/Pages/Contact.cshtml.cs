using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonAmourBistro.Models.Formulare;

namespace MonAmourBistro.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    public MesajContact Input { get; set; } = new();

    // Mesajul de confirmare supraviețuiește redirecționării (TempData).
    [TempData]
    public string? Confirmare { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // valorile introduse rămân, apar erorile
        }

        // Fără bază de date: mesajul nu este salvat, doar confirmăm primirea.
        Confirmare = $"Mulțumim, {Input.NumePrenume.Trim()}! Mesajul „{Input.Subiect.Trim()}” a fost trimis. Vă răspundem la {Input.Email.Trim()}.";
        return RedirectToPage(); // Post-Redirect-Get: formularul se golește
    }
}
