using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko
{
    public class StanowiskoGrupaNazwaOprogramowania : BaseEntity
    {
        [DisplayName("Grupa")]
        public string? Typ { get; set; }

        [DisplayName("Nazwa")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
