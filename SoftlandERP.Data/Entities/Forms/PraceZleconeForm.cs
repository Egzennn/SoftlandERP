using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms
{
    public class PraceZleconeForm : BaseEntity
    {
        [DisplayName("Dział zlecający")]
        public string? Dzial { get; set; }

        [DisplayName("IDS")]
        public string? IDS { get; set; }

        [DisplayName("Dokument")]
        public string? Dokument { get; set; }

        [DisplayName("Kategoria pracy")]
        public string? KategoriaPracy { get; set; }

        [DisplayName("Rodzaj pracy")]
        public string? RodzajPracy { get; set; }

        [DisplayName("Nazwa receptury")]
        public string? NazwaReceptury { get; set; }

        [DisplayName("Ilość pobrana [szt]")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Dozwolone tylko liczby")]
        public int? IloscPobrana { get; set; }

        [DisplayName("Ilość wykonana [szt]")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Dozwolone tylko liczby")]
        public int? IloscWykonana { get; set; }

        [DisplayName("Akronim wykonawcy")]
        public string? AkronimWyk { get; set; }

        [DisplayName("Data i godzina wydania zlecenia")]
        public DateTime? WydanieZlecenia { get; set; }

        [DisplayName("Data i godzina przyjęcia zlecenia")]
        public DateTime? PrzyjecieZlecenia { get; set; }

        [DisplayName("Czas wykonania zlecenia")]
        public TimeSpan? CzasWykonania { get; set; }

        [DisplayName("Wartość pracy zleconej [PLN]")]
        public decimal? WartoscPracyZleconej { get; set; }
    }
}
