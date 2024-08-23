using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftlandERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class KLStanowiskoZmianaNazwKolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkTime",
                table: "StanowiskoSprawyForms",
                newName: "CzasPracy");

            migrationBuilder.RenameColumn(
                name: "Verifier",
                table: "StanowiskoSprawyForms",
                newName: "Weryfikujacy");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "StanowiskoSprawyForms",
                newName: "Temat");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "StanowiskoSprawyForms",
                newName: "TerminWykonania");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "StanowiskoSprawyForms",
                newName: "Rola");

            migrationBuilder.RenameColumn(
                name: "Responsible",
                table: "StanowiskoSprawyForms",
                newName: "Odpowiedzialny");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "StanowiskoSprawyForms",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "StanowiskoSprawyForms",
                newName: "DataRozpoczecia");

            migrationBuilder.RenameColumn(
                name: "Completion",
                table: "StanowiskoSprawyForms",
                newName: "Realizacja");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "StanowiskoSprawyForms",
                newName: "Kategoria");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "StanowiskoOprogramowanieForm",
                newName: "Wersja");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "StanowiskoOprogramowanieForm",
                newName: "Rola");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StanowiskoOprogramowanieForm",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "StanowiskoOprogramowanieForm",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Installation",
                table: "StanowiskoOprogramowanieForm",
                newName: "Lokalizacja");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "StanowiskoOprogramowanieForm",
                newName: "Instalacja");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "StanowiskoOprogramowanieForm",
                newName: "Grupa");

            migrationBuilder.RenameColumn(
                name: "ShipmentType",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "Uwagi");

            migrationBuilder.RenameColumn(
                name: "ReceiptLocation",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "RodzajPrzesylki");

            migrationBuilder.RenameColumn(
                name: "ReceiptDate",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "DataOdbioru");

            migrationBuilder.RenameColumn(
                name: "DispatchDate",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "DataNadania");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "LokalizacjaOdbioru");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "StanowiskoObiektyITForms",
                newName: "Rola");

            migrationBuilder.RenameColumn(
                name: "ServiceGroup",
                table: "StanowiskoObiektyITForms",
                newName: "RodzajObiektu");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "StanowiskoObiektyITForms",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Objects",
                table: "StanowiskoObiektyITForms",
                newName: "Obiekty");

            migrationBuilder.RenameColumn(
                name: "ObjectType",
                table: "StanowiskoObiektyITForms",
                newName: "NazwaUslugi");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "StanowiskoObiektyITForms",
                newName: "GrupaUslug");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasure",
                table: "StanowiskoListaWypWSZForms",
                newName: "Rodzaj");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "StanowiskoListaWypWSZForms",
                newName: "Priorytet");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "StanowiskoListaWypWSZForms",
                newName: "Pomieszczenie");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StanowiskoListaWypWSZForms",
                newName: "Ilosc");

            migrationBuilder.RenameColumn(
                name: "ProductGroup",
                table: "StanowiskoListaWypWSZForms",
                newName: "JednostkaMiary");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "StanowiskoListaWypWSZForms",
                newName: "GrupaProduktu");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StanowiskoListaWypWSZForms",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "StanowiskoListaWypWSZForms",
                newName: "Lokalizacja");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasure",
                table: "StanowiskoListaWypForms",
                newName: "JednostkaMiary");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "StanowiskoListaWypForms",
                newName: "Rodzaj");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "StanowiskoListaWypForms",
                newName: "Priorytet");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StanowiskoListaWypForms",
                newName: "Ilosc");

            migrationBuilder.RenameColumn(
                name: "ProductGroup",
                table: "StanowiskoListaWypForms",
                newName: "GrupaProduktu");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "StanowiskoListaWypForms",
                newName: "Pomieszczenie");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StanowiskoListaWypForms",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "StanowiskoListaWypForms",
                newName: "Lokalizacja");

            migrationBuilder.RenameColumn(
                name: "Workspace",
                table: "StanowiskoDOWForms",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Responsible",
                table: "StanowiskoDOWForms",
                newName: "Odpowiedzialny");

            migrationBuilder.RenameColumn(
                name: "Object",
                table: "StanowiskoDOWForms",
                newName: "ObszarRoboczy");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StanowiskoDOWForms",
                newName: "Zdarzenie");

            migrationBuilder.RenameColumn(
                name: "Event",
                table: "StanowiskoDOWForms",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "StanowiskoDOWForms",
                newName: "Obiekt");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "StanowiskoDOWForms",
                newName: "Termin");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "KLXMLForms",
                newName: "Typ");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "KLXMLForms",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "TrialDate",
                table: "KLTerminKorForms",
                newName: "DataProby");

            migrationBuilder.RenameColumn(
                name: "Document",
                table: "KLTerminKorForms",
                newName: "Dokument");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "KLTerminKorForms",
                newName: "Waluta");

            migrationBuilder.RenameColumn(
                name: "CorrectionDeadline",
                table: "KLTerminKorForms",
                newName: "TerminKorekty");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "KLTerminKorForms",
                newName: "Kwota");

            migrationBuilder.RenameColumn(
                name: "TransactionBlock",
                table: "KLRozlForms",
                newName: "Waluta");

            migrationBuilder.RenameColumn(
                name: "OrderBlock",
                table: "KLRozlForms",
                newName: "BlokadaZamownia");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "KLRozlForms",
                newName: "BlokadaTransakcji");

            migrationBuilder.RenameColumn(
                name: "CreditLimit",
                table: "KLRozlForms",
                newName: "LimitKredytowy");

            migrationBuilder.RenameColumn(
                name: "CreditDays",
                table: "KLRozlForms",
                newName: "DniKredytowe");

            migrationBuilder.RenameColumn(
                name: "ConfirmationDate",
                table: "KLRozlForms",
                newName: "DataPotwierdzenia");

            migrationBuilder.RenameColumn(
                name: "AllowedIssueLimit",
                table: "KLRozlForms",
                newName: "LimitWydania");

            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "KLOsobaForms",
                newName: "ImieNazwisko");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "KLKKKForms",
                newName: "Waga");

            migrationBuilder.RenameColumn(
                name: "RelatedMatters",
                table: "KLKKKForms",
                newName: "Towar");

            migrationBuilder.RenameColumn(
                name: "Recommendation",
                table: "KLKKKForms",
                newName: "SprawyPowiazane");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "KLKKKForms",
                newName: "Rekomendacja");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "KLKKKForms",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "KLKKKForms",
                newName: "Termin");

            migrationBuilder.RenameColumn(
                name: "ServiceCycle",
                table: "KLKartaForms",
                newName: "CyklObslugi");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "KLKartaForms",
                newName: "Rynek");

            migrationBuilder.RenameColumn(
                name: "Market",
                table: "KLKartaForms",
                newName: "Opiekun");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "KLKartaForms",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "KLKartaForms",
                newName: "Uwagi");

            migrationBuilder.RenameColumn(
                name: "Caretaker",
                table: "KLKartaForms",
                newName: "Grupa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weryfikujacy",
                table: "StanowiskoSprawyForms",
                newName: "Verifier");

            migrationBuilder.RenameColumn(
                name: "TerminWykonania",
                table: "StanowiskoSprawyForms",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Temat",
                table: "StanowiskoSprawyForms",
                newName: "Topic");

            migrationBuilder.RenameColumn(
                name: "Rola",
                table: "StanowiskoSprawyForms",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Realizacja",
                table: "StanowiskoSprawyForms",
                newName: "Completion");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "StanowiskoSprawyForms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Odpowiedzialny",
                table: "StanowiskoSprawyForms",
                newName: "Responsible");

            migrationBuilder.RenameColumn(
                name: "Kategoria",
                table: "StanowiskoSprawyForms",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "DataRozpoczecia",
                table: "StanowiskoSprawyForms",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "CzasPracy",
                table: "StanowiskoSprawyForms",
                newName: "WorkTime");

            migrationBuilder.RenameColumn(
                name: "Wersja",
                table: "StanowiskoOprogramowanieForm",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "Rola",
                table: "StanowiskoOprogramowanieForm",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "StanowiskoOprogramowanieForm",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "StanowiskoOprogramowanieForm",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Lokalizacja",
                table: "StanowiskoOprogramowanieForm",
                newName: "Installation");

            migrationBuilder.RenameColumn(
                name: "Instalacja",
                table: "StanowiskoOprogramowanieForm",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "Grupa",
                table: "StanowiskoOprogramowanieForm",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Uwagi",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "ShipmentType");

            migrationBuilder.RenameColumn(
                name: "RodzajPrzesylki",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "ReceiptLocation");

            migrationBuilder.RenameColumn(
                name: "LokalizacjaOdbioru",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "DataOdbioru",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "ReceiptDate");

            migrationBuilder.RenameColumn(
                name: "DataNadania",
                table: "StanowiskoOdbiorPrzesylekForms",
                newName: "DispatchDate");

            migrationBuilder.RenameColumn(
                name: "Rola",
                table: "StanowiskoObiektyITForms",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "RodzajObiektu",
                table: "StanowiskoObiektyITForms",
                newName: "ServiceGroup");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "StanowiskoObiektyITForms",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Obiekty",
                table: "StanowiskoObiektyITForms",
                newName: "Objects");

            migrationBuilder.RenameColumn(
                name: "NazwaUslugi",
                table: "StanowiskoObiektyITForms",
                newName: "ObjectType");

            migrationBuilder.RenameColumn(
                name: "GrupaUslug",
                table: "StanowiskoObiektyITForms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Rodzaj",
                table: "StanowiskoListaWypWSZForms",
                newName: "UnitOfMeasure");

            migrationBuilder.RenameColumn(
                name: "Priorytet",
                table: "StanowiskoListaWypWSZForms",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Pomieszczenie",
                table: "StanowiskoListaWypWSZForms",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "StanowiskoListaWypWSZForms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Lokalizacja",
                table: "StanowiskoListaWypWSZForms",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "JednostkaMiary",
                table: "StanowiskoListaWypWSZForms",
                newName: "ProductGroup");

            migrationBuilder.RenameColumn(
                name: "Ilosc",
                table: "StanowiskoListaWypWSZForms",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "GrupaProduktu",
                table: "StanowiskoListaWypWSZForms",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "Rodzaj",
                table: "StanowiskoListaWypForms",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Priorytet",
                table: "StanowiskoListaWypForms",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "Pomieszczenie",
                table: "StanowiskoListaWypForms",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "StanowiskoListaWypForms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Lokalizacja",
                table: "StanowiskoListaWypForms",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "JednostkaMiary",
                table: "StanowiskoListaWypForms",
                newName: "UnitOfMeasure");

            migrationBuilder.RenameColumn(
                name: "Ilosc",
                table: "StanowiskoListaWypForms",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "GrupaProduktu",
                table: "StanowiskoListaWypForms",
                newName: "ProductGroup");

            migrationBuilder.RenameColumn(
                name: "Zdarzenie",
                table: "StanowiskoDOWForms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Termin",
                table: "StanowiskoDOWForms",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "StanowiskoDOWForms",
                newName: "Workspace");

            migrationBuilder.RenameColumn(
                name: "Odpowiedzialny",
                table: "StanowiskoDOWForms",
                newName: "Responsible");

            migrationBuilder.RenameColumn(
                name: "ObszarRoboczy",
                table: "StanowiskoDOWForms",
                newName: "Object");

            migrationBuilder.RenameColumn(
                name: "Obiekt",
                table: "StanowiskoDOWForms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "StanowiskoDOWForms",
                newName: "Event");

            migrationBuilder.RenameColumn(
                name: "Typ",
                table: "KLXMLForms",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "KLXMLForms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Waluta",
                table: "KLTerminKorForms",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "TerminKorekty",
                table: "KLTerminKorForms",
                newName: "CorrectionDeadline");

            migrationBuilder.RenameColumn(
                name: "Kwota",
                table: "KLTerminKorForms",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Dokument",
                table: "KLTerminKorForms",
                newName: "Document");

            migrationBuilder.RenameColumn(
                name: "DataProby",
                table: "KLTerminKorForms",
                newName: "TrialDate");

            migrationBuilder.RenameColumn(
                name: "Waluta",
                table: "KLRozlForms",
                newName: "TransactionBlock");

            migrationBuilder.RenameColumn(
                name: "LimitWydania",
                table: "KLRozlForms",
                newName: "AllowedIssueLimit");

            migrationBuilder.RenameColumn(
                name: "LimitKredytowy",
                table: "KLRozlForms",
                newName: "CreditLimit");

            migrationBuilder.RenameColumn(
                name: "DniKredytowe",
                table: "KLRozlForms",
                newName: "CreditDays");

            migrationBuilder.RenameColumn(
                name: "DataPotwierdzenia",
                table: "KLRozlForms",
                newName: "ConfirmationDate");

            migrationBuilder.RenameColumn(
                name: "BlokadaZamownia",
                table: "KLRozlForms",
                newName: "OrderBlock");

            migrationBuilder.RenameColumn(
                name: "BlokadaTransakcji",
                table: "KLRozlForms",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "ImieNazwisko",
                table: "KLOsobaForms",
                newName: "NameSurname");

            migrationBuilder.RenameColumn(
                name: "Waga",
                table: "KLKKKForms",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "Towar",
                table: "KLKKKForms",
                newName: "RelatedMatters");

            migrationBuilder.RenameColumn(
                name: "Termin",
                table: "KLKKKForms",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "SprawyPowiazane",
                table: "KLKKKForms",
                newName: "Recommendation");

            migrationBuilder.RenameColumn(
                name: "Rekomendacja",
                table: "KLKKKForms",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "KLKKKForms",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Uwagi",
                table: "KLKartaForms",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "Rynek",
                table: "KLKartaForms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Opiekun",
                table: "KLKartaForms",
                newName: "Market");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "KLKartaForms",
                newName: "Group");

            migrationBuilder.RenameColumn(
                name: "Grupa",
                table: "KLKartaForms",
                newName: "Caretaker");

            migrationBuilder.RenameColumn(
                name: "CyklObslugi",
                table: "KLKartaForms",
                newName: "ServiceCycle");
        }
    }
}
