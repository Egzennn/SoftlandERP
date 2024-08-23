using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class PraceZleconev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AkronimMag",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                newName: "RodzajPracy");

            migrationBuilder.RenameColumn(
                name: "AkronimLog",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                newName: "NazwaReceptury");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Czas",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddColumn<decimal>(
                name: "Kwota",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kwota",
                table: "OgolneRecepturyNormWykonawczychVocabulary");

            migrationBuilder.RenameColumn(
                name: "RodzajPracy",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                newName: "AkronimMag");

            migrationBuilder.RenameColumn(
                name: "NazwaReceptury",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                newName: "AkronimLog");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Czas",
                table: "OgolneRecepturyNormWykonawczychVocabulary",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }
    }
}
