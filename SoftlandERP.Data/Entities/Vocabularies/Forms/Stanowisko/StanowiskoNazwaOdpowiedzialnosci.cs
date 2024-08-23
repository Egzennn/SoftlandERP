using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko
{
    public class StanowiskoNazwaOdpowiedzialnosci : BaseEntity
    {
        [DisplayName("Symbol")]
        public string? Typ { get; set; }

        [DisplayName("Nazwa")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
