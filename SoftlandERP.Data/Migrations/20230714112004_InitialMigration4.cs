using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserAddressVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TowarSekcjeVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TowarSekcjeParametryWartoscVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TowarSekcjeParametryVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoSprawyForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoOprogramowanieForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoOdbiorPrzesylekForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoObiektyITForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoListaWypWSZForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoListaWypForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StanowiskoDOWForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaStatusVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaRodzajDokumentuVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaPrzewoznikVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaPlatnikVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaAkronimDokumentuVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SpedycjaAkronimCzynnosciPakowaniaVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OprogramowanieTypVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OprogramowanieInstalacjaVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OgolneStatusVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OgolneStanVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OgolneRolaVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KLXMLForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KLTerminKorForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KLOsobaForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KLKKKForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KLKartaForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "HelperTextVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DownloadFormVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentyWnioskiVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentyTematVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentySymbolDokumentuVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentyRodzajPrzesylkiVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentyRodzajKosztuVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DokumentyKosztObslugiVocabulary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CashflowForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ApplicationPermissionRules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KLRozlForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditDays = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<int>(type: "int", nullable: false),
                    AllowedIssueLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionBlock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderBlock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KLRozlForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KLRozlForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserAddressVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TowarSekcjeVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TowarSekcjeParametryWartoscVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TowarSekcjeParametryVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoSprawyForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoOprogramowanieForm");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoOdbiorPrzesylekForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoObiektyITForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoListaWypWSZForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoListaWypForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StanowiskoDOWForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaStatusVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaRodzajDokumentuVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaPrzewoznikVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaPlatnikVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaAkronimDokumentuVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SpedycjaAkronimCzynnosciPakowaniaVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OprogramowanieTypVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OprogramowanieInstalacjaVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OgolneStatusVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OgolneStanVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OgolneRolaVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KLXMLForm");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KLTerminKorForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KLOsobaForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KLKKKForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KLKartaForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HelperTextVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DownloadFormVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentyWnioskiVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentyTematVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentySymbolDokumentuVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentyRodzajPrzesylkiVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentyRodzajKosztuVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DokumentyKosztObslugiVocabulary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CashflowForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ApplicationPermissionRules");
        }
    }
}
