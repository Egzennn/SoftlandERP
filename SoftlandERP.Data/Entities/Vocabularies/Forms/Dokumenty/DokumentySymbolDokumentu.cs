using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty
{
    public class DokumentySymbolDokumentu : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}