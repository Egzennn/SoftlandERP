using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class PraceZleconev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpedycjaRodzajePracVocabulary");

            migrationBuilder.CreateTable(
                name: "OgolneKategoriaPracyVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneKategoriaPracyVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneNazwaRecepturyVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneNazwaRecepturyVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneRecepturyNormWykonawczychVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AkronimLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AkronimMag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Czas = table.Column<TimeSpan>(type: "time", nullable: false),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneRecepturyNormWykonawczychVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneRodzajPracyVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneRodzajPracyVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PraceZleconeForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dzial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dokument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriaPracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RodzajPracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NazwaReceptury = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IloscPobrana = table.Column<int>(type: "int", nullable: true),
                    IloscWykonana = table.Column<int>(type: "int", nullable: true),
                    AkronimWyk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WydanieZlecenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrzyjecieZlecenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CzasWykonania = table.Column<TimeSpan>(type: "time", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PraceZleconeForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgolneKategoriaPracyVocabulary");

            migrationBuilder.DropTable(
                name: "OgolneNazwaRecepturyVocabulary");

            migrationBuilder.DropTable(
                name: "OgolneRecepturyNormWykonawczychVocabulary");

            migrationBuilder.DropTable(
                name: "OgolneRodzajPracyVocabulary");

            migrationBuilder.DropTable(
                name: "PraceZleconeForms");

            migrationBuilder.CreateTable(
                name: "SpedycjaRodzajePracVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaRodzajePracVocabulary", x => x.Id);
                });
        }
    }
}
