using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationPermissionRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADGroupDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerOnly = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermissionRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyKosztObslugi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyKosztObslugi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyRodzajKosztu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyRodzajKosztu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyRodzajPrzesylki",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyRodzajPrzesylki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentySymbolDokumentu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentySymbolDokumentu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyTemat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyTemat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DokumentyWnioski",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DokumentyWnioski", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DownloadFormVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadFormVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelperTextVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldHelperText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperTextVocabulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MPKForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wlasciciel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MPKList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RodzajKosztu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrDokumentu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kwota = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Waluta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPKForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneRola",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneRola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneStan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneStan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgolneStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgolneStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OprogramowanieInstalacja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OprogramowanieInstalacja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OprogramowanieTyp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OprogramowanieTyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaAkronimCzynnosciPakowania",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaAkronimCzynnosciPakowania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaAkronimDokumentu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaAkronimDokumentu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaPlatnik",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaPlatnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaPrzewoznik",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaPrzewoznik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaRodzajDokumentu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaRodzajDokumentu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpedycjaStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpedycjaStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TowarSekcje",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TowarSekcje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TowarSekcjeParametry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TowarSekcjeParametry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TowarSekcjeParametryWartosc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wartosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Odpowiedzialny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TowarSekcjeParametryWartosc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddressVocabulary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoLetterISOCountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddressVocabulary", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "HelperTextVocabulary",
                columns: new[] { "Id", "Created", "CreatedBy", "Entity", "FieldDisplayName", "FieldHelperText", "FieldName", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("03a6f190-9d5e-4271-ba05-a1eaf3bc7f4b"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2772), null, "DownloadableForm", "Kategoria", null, "Category", null, null },
                    { new Guid("0bd4f094-5392-49e8-b2c2-d6c2fc8738f3"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2715), null, "UserAddress", "Street", null, "Street", null, null },
                    { new Guid("26689713-fce4-4f4b-a914-4eeb06d05345"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1559), null, "ADUser", "Akronim", null, "Login", null, null },
                    { new Guid("2b74d52e-909d-4204-abd6-c7ded0b9f49b"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1379), null, "ADUser", "Email", null, "EmailAddress", null, null },
                    { new Guid("5bd72ccf-25d6-4d48-abc5-1a624eeeb62e"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2104), null, "ADUser", "Stanowisko", null, "JobTitle", null, null },
                    { new Guid("7b703556-89e2-4077-96d1-f5488baf8af0"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1831), null, "ADUser", "Haslo nigdy nie wygasa", null, "PasswordNeverExpires", null, null },
                    { new Guid("7d52b771-e42b-4a57-8dbd-a4dd25f0b658"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1162), null, "ADUser", "Imię", null, "FirstName", null, null },
                    { new Guid("81019b56-8503-4ef6-8f89-d9feeb13d214"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1796), null, "ADUser", "Czy jest aktywny?", null, "Enabled", null, null },
                    { new Guid("8a823de6-579f-47a4-ad6c-edfc9b43dd35"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2629), null, "UserAddress", "Kraj", null, "Country", null, null },
                    { new Guid("8f1c5357-5f7c-4ce1-90e2-4fd36fe28e80"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2536), null, "ApplicationPermissionsRule", "Tylko przełożony", null, "ManagerOnly", null, null },
                    { new Guid("95b154a7-16c7-4e57-9cbb-a569807cdff1"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1866), null, "ADUser", "Użytkownik musi zmienić hasło przy następnym logowaniu", null, "UserMustChangePassword", null, null },
                    { new Guid("a2393df1-fa19-42ff-885f-8f92423c2d81"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2681), null, "UserAddress", "Miasto", null, "City", null, null },
                    { new Guid("b40d9656-e2a3-43cc-a561-5d5066c91e46"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2209), null, "ApplicationPermissionsRule", "Nazwa modulu", null, "Module", null, null },
                    { new Guid("b7bcb0f0-2739-4c63-b88f-4a5afd897d1f"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2033), null, "ADUser", "Firma", null, "Company", null, null },
                    { new Guid("b80620c3-a0bb-451c-9932-7e834469d642"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1423), null, "ADUser", "Telefon stanowiskowy", null, "Mobile", null, null },
                    { new Guid("b998ea5d-3503-49b3-b4b0-78a01a1ca17e"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1715), null, "ADUser", "Data wygaśnieńcia konta", null, "AccountExpirationDate", null, null },
                    { new Guid("ba6fb9af-1f73-4393-8cad-3722be0d26ed"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1941), null, "ADUser", "Address", null, "Address", null, null },
                    { new Guid("c35dad0c-bbb7-4a6c-a7cd-c1489df0a759"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2247), null, "ApplicationPermissionsRule", "Grupa Active Directory", null, "ADGroupDisplayName", null, null },
                    { new Guid("ce0f8135-d543-4ee0-aefd-4c696db4327e"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1502), null, "ADUser", "Telefon działowy", null, "DepartmentMobile", null, null },
                    { new Guid("cf77ddd2-c860-4573-86c3-619c2e0a6d0b"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2141), null, "ADUser", "Przyłożony", null, "Manager", null, null },
                    { new Guid("d241e088-2973-4417-a132-ab3312b2d7b7"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2808), null, "DownloadableForm", "Ścieżka", null, "Path", null, null },
                    { new Guid("dad57be0-3ed7-4a67-9647-041d144f6d9c"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1335), null, "ADUser", "Nazwisko", null, "LastName", null, null },
                    { new Guid("de3e4b5b-ffe1-4b81-b329-870b46176988"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1758), null, "ADUser", "Hasło", null, "Password", null, null },
                    { new Guid("e1ea6385-b63c-4d25-9f7c-fcbf09d505e2"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(2070), null, "ADUser", "Dział", null, "Department", null, null },
                    { new Guid("eb64fbbe-57ea-4454-b550-01233f0b07e7"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1901), null, "ADUser", "Użytkownik nie może zmienić hasło", null, "UserCannotChangePassword", null, null },
                    { new Guid("fc02a820-ec41-4bc6-ac33-6307b9725ed5"), new DateTime(2023, 6, 26, 13, 37, 26, 673, DateTimeKind.Local).AddTicks(1597), null, "ADUser", null, null, "AccountExpirationDateCheck", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationPermissionRules");

            migrationBuilder.DropTable(
                name: "DokumentyKosztObslugi");

            migrationBuilder.DropTable(
                name: "DokumentyRodzajKosztu");

            migrationBuilder.DropTable(
                name: "DokumentyRodzajPrzesylki");

            migrationBuilder.DropTable(
                name: "DokumentySymbolDokumentu");

            migrationBuilder.DropTable(
                name: "DokumentyTemat");

            migrationBuilder.DropTable(
                name: "DokumentyWnioski");

            migrationBuilder.DropTable(
                name: "DownloadFormVocabulary");

            migrationBuilder.DropTable(
                name: "HelperTextVocabulary");

            migrationBuilder.DropTable(
                name: "MPKForms");

            migrationBuilder.DropTable(
                name: "OgolneRola");

            migrationBuilder.DropTable(
                name: "OgolneStan");

            migrationBuilder.DropTable(
                name: "OgolneStatus");

            migrationBuilder.DropTable(
                name: "OprogramowanieInstalacja");

            migrationBuilder.DropTable(
                name: "OprogramowanieTyp");

            migrationBuilder.DropTable(
                name: "SpedycjaAkronimCzynnosciPakowania");

            migrationBuilder.DropTable(
                name: "SpedycjaAkronimDokumentu");

            migrationBuilder.DropTable(
                name: "SpedycjaPlatnik");

            migrationBuilder.DropTable(
                name: "SpedycjaPrzewoznik");

            migrationBuilder.DropTable(
                name: "SpedycjaRodzajDokumentu");

            migrationBuilder.DropTable(
                name: "SpedycjaStatus");

            migrationBuilder.DropTable(
                name: "TowarSekcje");

            migrationBuilder.DropTable(
                name: "TowarSekcjeParametry");

            migrationBuilder.DropTable(
                name: "TowarSekcjeParametryWartosc");

            migrationBuilder.DropTable(
                name: "UserAddressVocabulary");
        }
    }
}
