using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpedycjaKonfekcja2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Akronim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Czynnosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RodzajPrac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Akronim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Czynnosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RodzajPrac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaCzynnosciVocabulary",
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
                    table.PrimaryKey("PK_SpedycjaCzynnosciVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaRodzajePracVocabulary",
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
                    table.PrimaryKey("PK_SpedycjaRodzajePracVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaStandardyPrzygotowaniaVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AkronimLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AkronimMag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzasS = table.Column<int>(type: "int", nullable: true),
                    CzasMin = table.Column<int>(type: "int", nullable: true),
                    CzasRbg = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_SpedycjaStandardyPrzygotowaniaVocabulary", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary");

            migrationBuilder.DropTable(
                name: "SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary");

            migrationBuilder.DropTable(
                name: "SpedycjaCzynnosciVocabulary");

            migrationBuilder.DropTable(
                name: "SpedycjaRodzajePracVocabulary");

            migrationBuilder.DropTable(
                name: "SpedycjaStandardyPrzygotowaniaVocabulary");
        }
    }
}
