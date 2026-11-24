using System.ComponentModel.DataAnnotations;

namespace MonAmourBistro.Models.Formulare;

// Datele din formularul de autentificare (simulare, fără bază de date).
public class AutentificareInput
{
    [Required(ErrorMessage = "Introduceți adresa de e-mail.")]
    [EmailAddress(ErrorMessage = "Adresa de e-mail nu este validă.")]
    [StringLength(100, ErrorMessage = "E-mailul poate avea cel mult 100 de caractere.")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduceți parola.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Parola trebuie să aibă între 4 și 50 de caractere.")]
    [DataType(DataType.Password)]
    [Display(Name = "Parolă")]
    public string Parola { get; set; } = string.Empty;
}
