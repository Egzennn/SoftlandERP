using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko
{
    public class StanowiskoRodzajePrzesylek : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
