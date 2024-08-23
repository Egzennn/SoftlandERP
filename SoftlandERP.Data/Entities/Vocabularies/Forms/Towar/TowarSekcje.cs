using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Towar
{
    public class TowarSekcje : BaseEntity
    {
        [DisplayName("Sekcja")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}