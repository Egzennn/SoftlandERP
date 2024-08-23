using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne
{
    public class OgolneRecepturyNormWykonawczych : BaseEntity
    {
        [DisplayName("Rodzaj pracy")]
        public string? RodzajPracy { get; set; }

        [DisplayName("Nazwa receptury")]
        public string? NazwaReceptury { get; set; }

        [DisplayName("Czas")]
        public TimeSpan Czas { get; set; }

        [DisplayName("Stawka za RBG [PLN]")]
        [RegularExpression(@"^[\d,]+$", ErrorMessage = "Dozwolone tylko liczby i przecinek")]
        public decimal? Kwota { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
