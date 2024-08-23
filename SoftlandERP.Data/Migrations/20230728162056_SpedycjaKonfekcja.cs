using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpedycjaKonfekcja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KLXMLForm",
                table: "KLXMLForm");

            migrationBuilder.RenameTable(
                name: "KLXMLForm",
                newName: "KLXMLForms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KLXMLForms",
                table: "KLXMLForms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SpedycjaKonfekcjaForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AkronimLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sekcja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Towar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IloscZlecenia = table.Column<int>(type: "int", nullable: true),
                    AkronimWyk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaKonfekcjaForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpedycjaKonfekcjaForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KLXMLForms",
                table: "KLXMLForms");

            migrationBuilder.RenameTable(
                name: "KLXMLForms",
                newName: "KLXMLForm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KLXMLForm",
                table: "KLXMLForm",
                column: "Id");
        }
    }
}
