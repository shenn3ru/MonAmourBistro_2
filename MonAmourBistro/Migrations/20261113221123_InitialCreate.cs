using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonAmourBistro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Preparate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Denumire = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Categorie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Pret = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Gramaj = table.Column<int>(type: "int", nullable: false),
                    Disponibil = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparate", x => x.Id);
                    table.CheckConstraint("CK_Preparate_Gramaj", "Gramaj > 0");
                    table.CheckConstraint("CK_Preparate_Pret", "Pret > 0");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Preparate",
                columns: new[] { "Id", "Categorie", "Denumire", "Descriere", "Disponibil", "Gramaj", "Pret" },
                values: new object[,]
                {
                    { 1, "Supe", "Supă cremă de ciuperci", "Ciuperci champignon, smântână și crutoane.", true, 300, 65m },
                    { 2, "Salate", "Salată Caesar", "Salată romană, piept de pui, parmezan și sos Caesar.", true, 280, 95m },
                    { 3, "Paste", "Paste carbonara", "Spaghete, guanciale, gălbenuș și pecorino.", true, 320, 120m },
                    { 4, "Feluri principale", "Mușchi de vită cu legume", "Mușchi de vită la grătar, legume coapte și sos de piper.", true, 350, 245m },
                    { 5, "Deserturi", "Tiramisu", "Mascarpone, pișcoturi și cafea.", true, 150, 70m },
                    { 6, "Băuturi", "Limonadă de casă", "Lămâie, mentă și miere.", false, 400, 45m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preparate_Denumire",
                table: "Preparate",
                column: "Denumire",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preparate");
        }
    }
}
