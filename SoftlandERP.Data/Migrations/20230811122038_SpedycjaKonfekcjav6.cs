using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpedycjaKonfekcjav6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Czas",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Czas",
                table: "SpedycjaKonfekcjaForms",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Czas",
                table: "SpedycjaStandardyPrzygotowaniaVocabulary");

            migrationBuilder.DropColumn(
                name: "Czas",
                table: "SpedycjaKonfekcjaForms");
        }
    }
}
