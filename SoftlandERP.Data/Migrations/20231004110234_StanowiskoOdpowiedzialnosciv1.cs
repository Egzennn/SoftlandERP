using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class StanowiskoOdpowiedzialnosciv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StanowiskoOprogramowanieForm",
                table: "StanowiskoOprogramowanieForm");

            migrationBuilder.RenameTable(
                name: "StanowiskoOprogramowanieForm",
                newName: "StanowiskoOprogramowanieForms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StanowiskoOprogramowanieForms",
                table: "StanowiskoOprogramowanieForms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StanowiskoOdpowiedzialnosciForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiczbaInterwalow = table.Column<int>(type: "int", nullable: false),
                    Interwal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Przesuniecie = table.Column<int>(type: "int", nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KluczPrzydzialu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lokalizacja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dzial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AkronimWyk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cyklicznosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanowiskoOdpowiedzialnosciForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanowiskoOdpowiedzialnosciForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StanowiskoOprogramowanieForms",
                table: "StanowiskoOprogramowanieForms");

            migrationBuilder.RenameTable(
                name: "StanowiskoOprogramowanieForms",
                newName: "StanowiskoOprogramowanieForm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StanowiskoOprogramowanieForm",
                table: "StanowiskoOprogramowanieForm",
                column: "Id");
        }
    }
}
