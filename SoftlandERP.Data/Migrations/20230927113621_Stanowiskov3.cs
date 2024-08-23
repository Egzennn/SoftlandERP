using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class Stanowiskov3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "CzasPracy",
                table: "StanowiskoSprawyForms",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzasPracy",
                table: "StanowiskoSprawyForms");
        }
    }
}
