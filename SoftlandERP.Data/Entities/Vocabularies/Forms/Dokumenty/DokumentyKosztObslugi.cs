using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty
{
    public class DokumentyKosztObslugi : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}