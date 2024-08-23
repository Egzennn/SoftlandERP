using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Oprogramowanie
{
    public class OprogramowanieGrupaUslug : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}