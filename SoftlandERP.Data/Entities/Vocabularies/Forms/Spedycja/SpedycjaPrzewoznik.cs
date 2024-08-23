using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja
{
    public class SpedycjaPrzewoznik : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}