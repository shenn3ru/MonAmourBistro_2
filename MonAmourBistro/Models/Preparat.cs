using System.ComponentModel.DataAnnotations;

namespace MonAmourBistro.Models;

// Entitatea gestionată în modulul de administrare: un preparat din meniul restaurantului.
public class Preparat
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Denumirea este obligatorie.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Denumirea trebuie să aibă între 2 și 100 de caractere.")]
    [Display(Name = "Denumire")]
    public string Denumire { get; set; } = string.Empty;

    [Required(ErrorMessage = "Alegeți o categorie.")]
    [StringLength(50)]
    [Display(Name = "Categorie")]
    public string Categorie { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Descrierea poate avea cel mult 500 de caractere.")]
    [Display(Name = "Descriere")]
    public string? Descriere { get; set; }

    [Required(ErrorMessage = "Prețul este obligatoriu.")]
    [Range(0.01, 10000, ErrorMessage = "Prețul trebuie să fie între 0,01 și 10 000 lei.")]
    [Display(Name = "Preț (lei)")]
    public decimal? Pret { get; set; }

    [Required(ErrorMessage = "Gramajul este obligatoriu.")]
    [Range(1, 5000, ErrorMessage = "Gramajul trebuie să fie între 1 și 5000 g.")]
    [Display(Name = "Gramaj (g)")]
    public int? Gramaj { get; set; }

    [Display(Name = "Disponibil în meniu")]
    public bool Disponibil { get; set; } = true;

    // Categoriile permise (folosite în lista derulantă și la validare).
    public static readonly string[] Categorii =
    {
        "Supe", "Salate", "Feluri principale", "Paste", "Deserturi", "Băuturi"
    };
}
