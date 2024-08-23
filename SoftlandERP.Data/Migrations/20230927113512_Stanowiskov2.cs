using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class Stanowiskov2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzasPracy",
                table: "StanowiskoSprawyForms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CzasPracy",
                table: "StanowiskoSprawyForms",
                type: "int",
                nullable: true);
        }
    }
}
