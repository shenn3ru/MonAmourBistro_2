using Microsoft.EntityFrameworkCore;
using MonAmourBistro.Models;

namespace MonAmourBistro.Data;

// Contextul bazei de date (MySQL). Fiecare DbSet<T> corespunde unui tabel.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Preparat> Preparate => Set<Preparat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Preparat>(e =>
        {
            // Tabelul "Preparate" cu restricții la nivel de bază de date:
            // prețul și gramajul trebuie să fie pozitive.
            e.ToTable("Preparate", t =>
            {
                t.HasCheckConstraint("CK_Preparate_Pret", "Pret > 0");
                t.HasCheckConstraint("CK_Preparate_Gramaj", "Gramaj > 0");
            });

            e.HasKey(p => p.Id);                       // cheie primară, generată automat
            e.Property(p => p.Denumire).IsRequired().HasMaxLength(100);
            e.Property(p => p.Categorie).IsRequired().HasMaxLength(50);
            e.Property(p => p.Descriere).HasMaxLength(500);
            e.Property(p => p.Pret).IsRequired().HasPrecision(10, 2);   // decimal(10,2)
            e.Property(p => p.Gramaj).IsRequired();
            e.Property(p => p.Disponibil).IsRequired();

            // Două preparate nu pot avea aceeași denumire.
            e.HasIndex(p => p.Denumire).IsUnique();

            // Înregistrări demonstrative (intră în migrarea inițială).
            e.HasData(
                new Preparat { Id = 1, Denumire = "Supă cremă de ciuperci", Categorie = "Supe", Descriere = "Ciuperci champignon, smântână și crutoane.", Pret = 65m, Gramaj = 300, Disponibil = true },
                new Preparat { Id = 2, Denumire = "Salată Caesar", Categorie = "Salate", Descriere = "Salată romană, piept de pui, parmezan și sos Caesar.", Pret = 95m, Gramaj = 280, Disponibil = true },
                new Preparat { Id = 3, Denumire = "Paste carbonara", Categorie = "Paste", Descriere = "Spaghete, guanciale, gălbenuș și pecorino.", Pret = 120m, Gramaj = 320, Disponibil = true },
                new Preparat { Id = 4, Denumire = "Mușchi de vită cu legume", Categorie = "Feluri principale", Descriere = "Mușchi de vită la grătar, legume coapte și sos de piper.", Pret = 245m, Gramaj = 350, Disponibil = true },
                new Preparat { Id = 5, Denumire = "Tiramisu", Categorie = "Deserturi", Descriere = "Mascarpone, pișcoturi și cafea.", Pret = 70m, Gramaj = 150, Disponibil = true },
                new Preparat { Id = 6, Denumire = "Limonadă de casă", Categorie = "Băuturi", Descriere = "Lămâie, mentă și miere.", Pret = 45m, Gramaj = 400, Disponibil = false }
            );
        });
    }
}
