using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Oprogramowanie
{
    public class OprogramowanieTyp : BaseEntity
    {
        [DisplayName("Typ")]
        public string? Typ { get; set; }

        [DisplayName("Program")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}