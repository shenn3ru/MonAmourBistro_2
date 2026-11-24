using System.ComponentModel.DataAnnotations;

namespace MonAmourBistro.Models.Formulare;

// Formularul specific temei: rezervarea unei mese la restaurant.
public class RezervareInput
{
    [Required(ErrorMessage = "Introduceți numele.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 100 de caractere.")]
    [Display(Name = "Nume și prenume")]
    public string Nume { get; set; } = string.Empty;

    [Required(ErrorMessage = "Introduceți numărul de telefon.")]
    [Phone(ErrorMessage = "Numărul de telefon nu este valid.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Telefonul trebuie să aibă între 6 și 20 de caractere.")]
    [Display(Name = "Telefon")]
    public string Telefon { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Adresa de e-mail nu este validă.")]
    [StringLength(100)]
    [Display(Name = "E-mail (opțional)")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Alegeți data.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data")]
    public DateTime? Data { get; set; }

    [Required(ErrorMessage = "Alegeți ora.")]
    [DataType(DataType.Time)]
    [Display(Name = "Ora")]
    public TimeSpan? Ora { get; set; }

    [Required(ErrorMessage = "Alegeți numărul de persoane.")]
    [Range(1, 12, ErrorMessage = "Se pot rezerva între 1 și 12 locuri.")]
    [Display(Name = "Număr de persoane")]
    public int? NrPersoane { get; set; } = 2;

    [StringLength(300, ErrorMessage = "Mențiunile pot avea cel mult 300 de caractere.")]
    [Display(Name = "Mențiuni (opțional)")]
    public string? Mentiuni { get; set; }
}
