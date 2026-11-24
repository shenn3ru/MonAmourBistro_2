using System.ComponentModel.DataAnnotations;

namespace MonAmourBistro.Models.Formulare;

// Datele din formularul de contact.
public class MesajContact
{
    [Required(ErrorMessage = "Introduceți numele și prenumele.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere.")]
    [Display(Name = "Nume și prenume")]
    public string NumePrenume { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduceți adresa de e-mail.")]
    [EmailAddress(ErrorMessage = "Adresa de e-mail nu este validă.")]
    [StringLength(100)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduceți subiectul mesajului.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Subiectul trebuie să aibă între 3 și 150 de caractere.")]
    [Display(Name = "Subiectul mesajului")]
    public string Subiect { get; set; } = string.Empty;

    [Required(ErrorMessage = "Scrieți mesajul.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Mesajul trebuie să aibă între 10 și 1000 de caractere.")]
    [Display(Name = "Mesaj")]
    public string Mesaj { get; set; } = string.Empty;
}
