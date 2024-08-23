using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpedycjaKonfekcjav5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IloscMagazynowa",
                table: "SpedycjaKonfekcjaForms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Max_Ilosc",
                table: "SpedycjaKonfekcjaForms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Min_Ilosc",
                table: "SpedycjaKonfekcjaForms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Suma_Sprzedazy",
                table: "SpedycjaKonfekcjaForms",
                type: "int",
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "SpedycjaKonfekcjaSekcje",
            //    columns: table => new
            //    {
            //        Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Sekcja = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Twr_Kod = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IloscMagazynowa = table.Column<int>(type: "int", nullable: true),
            //        Suma_Sprzedazy = table.Column<int>(type: "int", nullable: true),
            //        Min_Ilosc = table.Column<int>(type: "int", nullable: true),
            //        Max_Ilosc = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SpedycjaKonfekcjaSekcje");

            migrationBuilder.DropColumn(
                name: "IloscMagazynowa",
                table: "SpedycjaKonfekcjaForms");

            migrationBuilder.DropColumn(
                name: "Max_Ilosc",
                table: "SpedycjaKonfekcjaForms");

            migrationBuilder.DropColumn(
                name: "Min_Ilosc",
                table: "SpedycjaKonfekcjaForms");

            migrationBuilder.DropColumn(
                name: "Suma_Sprzedazy",
                table: "SpedycjaKonfekcjaForms");
        }
    }
}
