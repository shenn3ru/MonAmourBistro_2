using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MonAmourBistro.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Baza de date MySQL (provider oficial Oracle: MySql.EntityFrameworkCore).
// Șirul de conexiune este în appsettings.json -> ConnectionStrings:DefaultConnection.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!)
           // Dacă modelul diferă de migrări, afișăm un avertisment în loc să oprim aplicația.
           .ConfigureWarnings(w => w.Log(RelationalEventId.PendingModelChangesWarning)));

var app = builder.Build();

// Aplică automat migrările la pornire (creează baza "monamourbistro" și tabelul dacă nu există).
// Echivalent cu: Update-Database  /  dotnet ef database update
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Database.GetMigrations().Any())
    {
        throw new InvalidOperationException(
            "Nu există nicio migrare. Rulați în Package Manager Console: Add-Migration InitialCreate, apoi Update-Database.");
    }
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/eroare");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
