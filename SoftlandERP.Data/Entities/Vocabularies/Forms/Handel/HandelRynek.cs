using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Handel
{
    public class HandelRynek : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
