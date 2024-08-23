using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpedycjaKonfekcjav7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzasMin",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary");

            migrationBuilder.DropColumn(
                name: "CzasRbg",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary");

            migrationBuilder.DropColumn(
                name: "CzasS",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary");

            migrationBuilder.DropColumn(
                name: "CzasRbg",
                table: "SpedycjaKonfekcjaForms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CzasMin",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CzasRbg",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CzasS",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CzasRbg",
                table: "SpedycjaKonfekcjaForms",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);
        }
    }
}
