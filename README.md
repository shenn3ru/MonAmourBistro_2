# MonAmourBistro

Site de prezentare pentru restaurantul Mon Amour Bistro (ASP.NET Core, .NET 10).

## Laboratorul 4–5: modulul „Administrare” → Gestiunea meniului (CRUD)

Entitatea gestionată: **Preparat** (`Models/Preparat.cs`)

| Câmp | Tip | Reguli |
|---|---|---|
| Id | int | cheie primară, generată automat |
| Denumire | text (max 100) | obligatoriu, 2–100 caractere, unică |
| Categorie | text (max 50) | obligatoriu, din lista permisă |
| Descriere | text (max 500) | opțional |
| Pret | decimal(10,2) | obligatoriu, 0,01 – 10 000 lei |
| Gramaj | int | obligatoriu, 1 – 5000 g |
| Disponibil | bool | — |

### Structura

- `Pages/Admin/Preparate/` – pagini Razor + Code Behind: `Index` (listă + căutare), `Create`, `Details`, `Edit`, `Delete`
- `Data/AppDbContext.cs` – contextul EF Core, `DbSet<Preparat>`, restricții (CHECK, index unic) și 6 înregistrări demonstrative (`HasData`)
- `Migrations/` – migrarea inițială `InitialCreate` (se generează, vezi mai jos)
- `appsettings.json` – `ConnectionStrings:DefaultConnection` (MySQL)
- `Program.cs` – `AddRazorPages`, `AddDbContext<AppDbContext>(UseMySQL)`, aplicarea migrărilor la pornire

Baza de date: **MySQL 8** (provider oficial Oracle `MySql.EntityFrameworkCore` 10.0.12).

Versiunea din laboratorul 4 (colecție `List<Preparat>` în `Services/PreparatService.cs`, înregistrată ca Singleton) se găsește în commit-ul „Laboratorul 4”.

### Rulare

1. Autentificați-vă ca **Administrator** (bara de sus → „Schimbă rolul”).
2. Meniul **Administrare** → tab-ul **Meniu** (`/admin/preparate`).

Pași înainte de prima rulare:

1. Porniți serverul MySQL și verificați utilizatorul/parola din `appsettings.json`:
   `Server=localhost;Port=3306;Database=monamourbistro;User=root;Password=;`
   (baza `monamourbistro` este creată automat de migrare).
2. În Visual Studio → Tools → NuGet Package Manager → **Package Manager Console**:

```
Add-Migration InitialCreate
Update-Database
```

sau din terminal: `dotnet ef migrations add InitialCreate` și `dotnet ef database update`.

3. Verificați tabelul în MySQL Workbench: `DESCRIBE Preparate;` și `SELECT * FROM Preparate;`

### Scenarii de verificare

| Scenariu | Rezultat așteptat |
|---|---|
| Adăugare date valide | se salvează și apare în listă, cu mesaj de succes |
| Formular invalid (ex. preț negativ, câmp gol) | erori lângă câmpuri, datele nu se salvează |
| Editare | valorile noi apar în listă și în „Detalii” |
| Anulează la editare | datele rămân neschimbate |
| Confirmare ștergere (POST) | înregistrarea dispare din baza de date |
| Anulează la ștergere | înregistrarea rămâne |
| Căutare după denumire | doar înregistrările potrivite / mesaj „nu a fost găsit” |
| Id inexistent (ex. `/admin/preparate/detalii/999`) | revenire la listă cu mesaj, fără blocare |
| Repornirea aplicației | datele se păstrează în MySQL |
