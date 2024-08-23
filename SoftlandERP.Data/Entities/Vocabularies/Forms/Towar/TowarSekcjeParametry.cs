using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Towar
{
    public class TowarSekcjeParametry : BaseEntity
    {
        [DisplayName("Sekcja")]
        public string? Typ { get; set; }

        [DisplayName("Parametr")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}